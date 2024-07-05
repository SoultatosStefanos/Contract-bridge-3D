using System;
using System.Collections;
using ContractBridge.Core;
using Events;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace LifeCycleManagers
{
    public class UILifeCycleManager : MonoBehaviour
    {
        [FormerlySerializedAs("Setup Canvas")]
        [SerializeField]
        private GameObject setupCanvas;

        [FormerlySerializedAs("Auction Canvas")]
        [SerializeField]
        private GameObject auctionCanvas;

        [FormerlySerializedAs("Game Canvas")]
        [SerializeField]
        private GameObject gameCanvas;

        [FormerlySerializedAs("Auction Transition Delay")]
        [SerializeField]
        private float auctionTransitionDelay = 0.5F;

        [FormerlySerializedAs("Game Transition Delay")]
        [SerializeField]
        private float gameTransitionDelay = 1.0F;

        [Inject]
        private IEventBus _eventBus;

        private void OnEnable()
        {
            _eventBus?.On<SessionPhaseChangedEvent>(HandleSessionChangedEvent);
        }

        private void OnDisable()
        {
            _eventBus?.Off<SessionPhaseChangedEvent>(HandleSessionChangedEvent);
        }

        private void HandleSessionChangedEvent(SessionPhaseChangedEvent evt)
        {
            switch (evt.Phase)
            {
                case Phase.Auction:
                    HandleAuctionTransition();
                    break;

                case Phase.Play:
                    HandleGameTransition();
                    break;

                case Phase.Setup:
                case Phase.Scoring:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleAuctionTransition()
        {
            StartCoroutine(WaitAndThen(auctionTransitionDelay, () =>
            {
                setupCanvas.SetActive(false);
                auctionCanvas.SetActive(true);
            }));
        }

        private void HandleGameTransition()
        {
            StartCoroutine(WaitAndThen(gameTransitionDelay, () =>
            {
                auctionCanvas.SetActive(false);
                gameCanvas.SetActive(true);
            }));
        }

        private static IEnumerator WaitAndThen(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);

            action.Invoke();
        }
    }
}