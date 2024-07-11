using System.Collections.Generic;
using System.Linq;
using ContractBridge.Solver;
using Domain;
using Events;
using Extensions;
using TMPro;
using UnityEngine;
using Wrappers;
using Zenject;

namespace Presenters
{
    public class MakeableContractsPresenter : MonoBehaviour
    {
        private const string MakeableContractsPanelTag = "Makeable Contracts / Any";

        private const string MakeableContractsLevelTextTag = "Makeable Contracts / Level Text";

        [Inject]
        private IAuctionExtras _auctionExtras;

        [Inject]
        private IEventBus _eventBus;

        private GameObject[] _panels;

        private void Awake()
        {
            _panels = gameObject.FindChildrenInHierarchy()
                .Where(child => child.CompareTag(MakeableContractsPanelTag))
                .ToArray();
        }

        private void OnEnable()
        {
            _eventBus.On<AuctionExtrasContractsSolutionEvent>(HandleAuctionExtrasSolutionSetEvent);

            if (_auctionExtras.Solution is { } solution)
            {
                UpdateVisual(solution);
            }
        }

        private void OnDisable()
        {
            _eventBus.Off<AuctionExtrasContractsSolutionEvent>(HandleAuctionExtrasSolutionSetEvent);
        }

        private void HandleAuctionExtrasSolutionSetEvent(AuctionExtrasContractsSolutionEvent e)
        {
            UpdateVisual(e.Solution);
        }

        // NOTE: Would be more efficient to traverse the contracts and invert the math, but whatever, still peanuts.
        private void UpdateVisual(IDoubleDummyContractsSolution solution)
        {
            foreach (var (panel, i) in AllLevelPanelsWithIndices())
            {
                Debug.Assert(IsLevelPanelIndex(i), "IsLevelPanelIndex(i)");

                var seatIndex = SeatIndex(i);
                var seatPanel = _panels[seatIndex];
                var seatWrapper = seatPanel.GetComponent<SeatWrapper>();
                var seat = seatWrapper.Seat;

                var denominationIndex = DenominationIndex(i);
                var denominationPanel = _panels[denominationIndex];
                var denominationWrapper = denominationPanel.GetComponent<DenominationWrapper>();
                var denomination = denominationWrapper.Denomination;

                if (solution.MakeableContract(seat, denomination) is not { } contract)
                {
                    continue;
                }

                var panelTextGameObject = FindChildByTag(panel, MakeableContractsLevelTextTag);
                var panelText = panelTextGameObject.GetComponent<TextMeshProUGUI>();
                panelText.text = contract.Level.ToNumeralString();
            }
        }

        private IEnumerable<(GameObject, int)> AllLevelPanelsWithIndices()
        {
            return _panels
                .Select((panel, index) => new { Panel = panel, Index = index })
                .Where(item => IsLevelPanelIndex(item.Index))
                .Select(item => (item.Panel, item.Index));
        }

        private static bool IsLevelPanelIndex(int i)
        {
            return i % 6 != 0 && i >= 7;
        }

        private static int SeatIndex(int i)
        {
            return i - i % 6;
        }

        private static int DenominationIndex(int i)
        {
            return i - (i - i % 6);
        }

        private static GameObject FindChildByTag(GameObject parent, string tag)
        {
            return parent
                .FindChildrenInHierarchy()
                .FirstOrDefault(child => child.CompareTag(tag));
        }
    }
}