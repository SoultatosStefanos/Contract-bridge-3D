using System;
using System.Linq;
using ContractBridge.Core;
using Events;
using UnityEngine;
using Zenject;

namespace Presenters
{
    public class AuctionBidsPresenter : MonoBehaviour
    {
        private const int SeatCount = 4;

        private const string BidPanelNamePrefix = "Bid Panel";

        private GameObject[] _bidPanels;

        private int _eastPlayCount;

        [Inject]
        private IEventBus _eventBus;

        private int _northPlayCount;

        private int _southPlayCount;

        private int _westPlayCount;

        private void Start()
        {
            _bidPanels = transform.Cast<Transform>()
                .Where(child => child.name.StartsWith(BidPanelNamePrefix))
                .Select(child => child.gameObject)
                .ToArray();
        }

        private void OnEnable()
        {
            _eventBus.On<AuctionCallEvent>(HandleAuctionCallEvent);
            _eventBus.On<AuctionPassEvent>(HandleAuctionPassEvent);
            _eventBus.On<AuctionDoubleEvent>(HandleAuctionDoubleEvent);
            _eventBus.On<AuctionRedoubleEvent>(HandleAuctionRedoubleEvent);
        }

        private void OnDisable()
        {
            _eventBus.Off<AuctionCallEvent>(HandleAuctionCallEvent);
            _eventBus.Off<AuctionPassEvent>(HandleAuctionPassEvent);
            _eventBus.Off<AuctionDoubleEvent>(HandleAuctionDoubleEvent);
            _eventBus.Off<AuctionRedoubleEvent>(HandleAuctionRedoubleEvent);
        }

        private void HandleAuctionCallEvent(AuctionCallEvent e)
        {
            UpdateAuctionBidsWithCall(e.Seat, e.Bid);
        }

        private void HandleAuctionDoubleEvent(AuctionDoubleEvent e)
        {
            UpdateAuctionBidsWithDouble(e.Seat);
        }

        private void HandleAuctionRedoubleEvent(AuctionRedoubleEvent e)
        {
            UpdateAuctionBidsWithDouble(e.Seat);
        }

        private void HandleAuctionPassEvent(AuctionPassEvent e)
        {
            UpdateAuctionBidsWithPass(e.Seat);
        }

        private void UpdateAuctionBidsWithCall(Seat seat, IBid bid)
        {
            UpdateAuctionBids(seat, panel =>
            {
                Debug.Log($"Updating panel: {panel.name}");
                // TODO
            });
        }

        private void UpdateAuctionBidsWithPass(Seat seat)
        {
            UpdateAuctionBids(seat, panel =>
            {
                Debug.Log($"Updating panel: {panel.name}");
                // TODO
            });
        }

        private void UpdateAuctionBidsWithDouble(Seat seat)
        {
            UpdateAuctionBids(seat, panel =>
            {
                Debug.Log($"Updating panel: {panel.name}");
                // TODO
            });
        }

        private void UpdateAuctionBids(Seat seat, Action<GameObject> updateAction)
        {
            var bidPanelIndex = NextBidPanelIndex(seat);
            updateAction.Invoke(_bidPanels[bidPanelIndex]);
            IncrementSeatPlayCount(seat);
        }

        private int NextBidPanelIndex(Seat seat)
        {
            return StartBidPanelSeatIndex(seat) + SeatPlayCount(seat) * SeatCount;
        }

        private int SeatPlayCount(Seat seat)
        {
            return seat switch
            {
                Seat.East => _eastPlayCount,
                Seat.South => _southPlayCount,
                Seat.West => _westPlayCount,
                Seat.North => _northPlayCount,
                _ => throw new ArgumentOutOfRangeException(nameof(seat), seat, null)
            };
        }

        private void IncrementSeatPlayCount(Seat seat)
        {
            switch (seat)
            {
                case Seat.East:
                    ++_eastPlayCount;
                    break;

                case Seat.South:
                    ++_southPlayCount;
                    break;

                case Seat.West:
                    ++_westPlayCount;
                    break;

                case Seat.North:
                    ++_northPlayCount;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(seat), seat, null);
            }
        }

        private static int StartBidPanelSeatIndex(Seat seat)
        {
            return seat switch
            {
                Seat.East => 0,
                Seat.South => 1,
                Seat.West => 2,
                Seat.North => 3,
                _ => throw new ArgumentOutOfRangeException(nameof(seat), seat, null)
            };
        }
    }
}