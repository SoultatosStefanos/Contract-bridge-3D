using System;
using System.Linq;
using ContractBridge.Core;
using Events;
using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Presenters
{
    public class AuctionBiddingHistoryPresenter : MonoBehaviour
    {
        private const int SeatCount = 4;

        private const int MaxAllowedPlayCount = 4;

        private const string BiddingHistoryPanelTag = "Bidding History / Panel";

        private const string BiddingHistoryPassDoubleTextTag = "Bidding History / Pass or Double Text";

        private const string BiddingHistoryRankTextTag = "Bidding History / Rank Text";

        private const string BiddingHistoryNtTextTag = "Bidding History / NT Text";

        private const string BiddingSuitImageTag = "Bidding History / Suit Image";

        private const string PassCaption = "Pass";

        private const string DoubleCaption = "Double";

        private const string NtCaption = "NT";

        [FormerlySerializedAs("Club Suit")]
        [SerializeField]
        private Sprite clubSuit;

        [FormerlySerializedAs("Diamonds Suit")]
        [SerializeField]
        private Sprite diamondsSuit;

        [FormerlySerializedAs("Spades Suit")]
        [SerializeField]
        private Sprite spadesSuit;

        [FormerlySerializedAs("Hearts Suit")]
        [SerializeField]
        private Sprite heartsSuit;

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
                .Where(child => child.CompareTag(BiddingHistoryPanelTag))
                .Select(child => child.gameObject)
                .ToArray();

            Debug.Assert(_bidPanels.Length == 16);
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
                var rankTextGameObject = FindChildByTag(panel, BiddingHistoryRankTextTag);

                Debug.Assert(!rankTextGameObject.activeSelf);

                rankTextGameObject.SetActive(true);

                var rankText = rankTextGameObject.GetComponent<TextMeshProUGUI>();
                rankText.text = bid.Level.ToNumeralString();

                if (bid.Denomination == Denomination.NoTrumps)
                {
                    var ntGameObject = FindChildByTag(panel, BiddingHistoryNtTextTag);

                    Debug.Assert(!ntGameObject.activeSelf);

                    ntGameObject.SetActive(true);

                    var ntText = ntGameObject.GetComponent<TextMeshProUGUI>();
                    ntText.text = NtCaption;
                }
                else
                {
                    var suitGameObject = FindChildByTag(panel, BiddingSuitImageTag);

                    Debug.Assert(!suitGameObject.activeSelf);

                    suitGameObject.SetActive(true);

                    var suitImage = suitGameObject.GetComponent<Image>();
                    suitImage.sprite = SuitSprite(bid.Denomination);
                }
            });
        }

        private Sprite SuitSprite(Denomination denomination)
        {
            Debug.Assert(denomination != Denomination.NoTrumps);

            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
            return denomination switch
            {
                Denomination.Clubs => clubSuit,
                Denomination.Diamonds => diamondsSuit,
                Denomination.Hearts => heartsSuit,
                Denomination.Spades => spadesSuit,
                _ => throw new ArgumentOutOfRangeException(nameof(denomination), denomination, null)
            };
        }

        private void UpdateAuctionBidsWithPass(Seat seat)
        {
            UpdateAuctionBids(seat, panel =>
            {
                var passTextGameObject = FindChildByTag(panel, BiddingHistoryPassDoubleTextTag);

                Debug.Assert(!passTextGameObject.activeSelf);

                passTextGameObject.SetActive(true);

                var passText = passTextGameObject.GetComponent<TextMeshProUGUI>();
                passText.text = PassCaption;
            });
        }

        private void UpdateAuctionBidsWithDouble(Seat seat)
        {
            UpdateAuctionBids(seat, panel =>
            {
                var doubleTextGameObject = FindChildByTag(panel, BiddingHistoryPassDoubleTextTag);

                Debug.Assert(!doubleTextGameObject.activeSelf);

                doubleTextGameObject.SetActive(true);

                var doubleText = doubleTextGameObject.GetComponent<TextMeshProUGUI>();
                doubleText.text = DoubleCaption;
            });
        }

        private void UpdateAuctionBids(Seat seat, Action<GameObject> updateAction)
        {
            CheckForBidPanelClear();

            var bidPanelIndex = NextBidPanelIndex(seat);
            Debug.Assert(bidPanelIndex is >= 0 and <= SeatCount * SeatCount);

            var bidPanel = _bidPanels[bidPanelIndex];

            Debug.Assert(!bidPanel.activeSelf);

            bidPanel.SetActive(true);
            updateAction.Invoke(bidPanel);

            IncrementSeatPlayCount(seat);
        }

        private int NextBidPanelIndex(Seat seat)
        {
            return FirstBidPanelSeatIndex(seat) + SeatPlayCount(seat) % MaxAllowedPlayCount * SeatCount;
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

        private void CheckForBidPanelClear()
        {
            if (_bidPanels.All(panel => panel.activeSelf))
            {
                ClearBidPanels();
            }
        }

        private void ClearBidPanels()
        {
            foreach (var panel in _bidPanels)
            {
                var ntGameObject = FindChildByTag(panel, BiddingHistoryNtTextTag);
                ntGameObject.SetActive(false);

                var suitGameObject = FindChildByTag(panel, BiddingSuitImageTag);
                suitGameObject.SetActive(false);

                var passTextGameObject = FindChildByTag(panel, BiddingHistoryPassDoubleTextTag);
                passTextGameObject.SetActive(false);

                var doubleTextGameObject = FindChildByTag(panel, BiddingHistoryPassDoubleTextTag);
                doubleTextGameObject.SetActive(false);

                panel.SetActive(false);
            }
        }

        private static int FirstBidPanelSeatIndex(Seat seat)
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

        private static GameObject FindChildByTag(GameObject parent, string tag)
        {
            return parent
                .FindChildrenInHierarchy()
                .FirstOrDefault(child => child.CompareTag(tag));
        }
    }
}