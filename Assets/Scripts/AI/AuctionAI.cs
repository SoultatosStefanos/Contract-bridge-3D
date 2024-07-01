using System.Collections;
using ContractBridge.Core;
using Events;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Debug = System.Diagnostics.Debug;

// TODO Work with predefined set contract, instead of always passing.

namespace AI
{
    public class AuctionAI : MonoBehaviour
    {
        [FormerlySerializedAs("Delay")]
        [SerializeField]
        [Tooltip("The time to wait (in seconds) before invoking the AI action at each turn.")]
        private float delay = 2.0f;

        [FormerlySerializedAs("Player Seat")]
        [SerializeField]
        private Seat playerSeat;

        [Inject]
        private IEventBus _eventBus;

        [Inject]
        private ISession _session;

        private void OnEnable()
        {
            _eventBus.On<AuctionTurnChangeEvent>(HandleAuctionTurnChangeEvent);
        }

        private void OnDisable()
        {
            _eventBus.Off<AuctionTurnChangeEvent>(HandleAuctionTurnChangeEvent);
        }

        private void HandleAuctionTurnChangeEvent(AuctionTurnChangeEvent e)
        {
            var isPlayersTurn = e.Seat == playerSeat;

            if (isPlayersTurn)
            {
                return;
            }

            StartCoroutine(TakeTurn(e.Seat));
        }

        private IEnumerator TakeTurn(Seat aiSeat)
        {
            yield return new WaitForSeconds(delay);

            Debug.Assert(_session.Auction != null, "_session.Auction != null");
            _session.Auction.Pass(aiSeat);
        }
    }
}