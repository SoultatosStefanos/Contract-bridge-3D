using System.Collections;
using ContractBridge.Core;
using Domain;
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
        private IAuctionExtras _auctionExtras;

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
            if (e.Seat == playerSeat)
            {
                return;
            }

            StartCoroutine(TakeTurn(e.Seat));
        }

        private IEnumerator TakeTurn(Seat aiSeat)
        {
            yield return new WaitForSeconds(delay);

            var auction = _session.Auction;
            Debug.Assert(auction != null, "auction != null");

            if (_auctionExtras.PickedContract is { } pickedContract)
            {
                if (pickedContract.Declarer != aiSeat)
                {
                    TryPass(auction, aiSeat); // Not this "bot's" turn!
                    yield break;
                }

                Debug.Assert(pickedContract.Risk == null, "Not handling doubling/redoubling!");

                if (auction.CanCall(pickedContract, aiSeat)) // Maybe the player ignored his choice!
                {
                    auction.Call(pickedContract, aiSeat);
                }
                else
                {
                    TryPass(auction, aiSeat); // What to do...
                }
            }
            else
            {
                TryPass(auction, aiSeat); // Always pass if no contract has been picked.
            }
        }

        private static void TryPass(IAuction auction, Seat aiSeat)
        {
            if (auction.CanPass(aiSeat))
            {
                auction.Pass(aiSeat);
            }
        }
    }
}