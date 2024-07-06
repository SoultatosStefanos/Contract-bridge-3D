using System;
using ContractBridge.Core;
using Controllers;
using Events;
using Registries;
using UnityEngine;
using Zenject;

namespace LifeCycleManagers
{
    public class CardControllerLifeCycleManager : MonoBehaviour
    {
        [Inject]
        private ICardGameObjectRegistry _cardGameObjectRegistry;

        [Inject]
        private IDeck _deck;

        [Inject]
        private IEventBus _eventBus;

        private void OnEnable()
        {
            _eventBus.On<SessionPhaseChangedEvent>(HandleSessionPhaseChangedEvent);
        }

        private void OnDisable()
        {
            _eventBus.Off<SessionPhaseChangedEvent>(HandleSessionPhaseChangedEvent);
        }

        private void HandleSessionPhaseChangedEvent(SessionPhaseChangedEvent evt)
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
            foreach (var card in _deck)
            {
                var cardGameObject = _cardGameObjectRegistry.GetGameObject(card);

                var cardPopUpComponent = cardGameObject.GetComponent<CardPopUpController>();
                cardPopUpComponent.enabled = true;
            }
        }

        private void HandleGameTransition()
        {
            foreach (var card in _deck)
            {
                var cardGameObject = _cardGameObjectRegistry.GetGameObject(card);

                var cardFollowComponent = cardGameObject.GetComponent<CardFollowController>();
                cardFollowComponent.enabled = true;
            }
        }
    }
}