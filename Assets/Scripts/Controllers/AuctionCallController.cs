using Components;
using ContractBridge.Core;
using ContractBridge.Core.Impl;
using Groups;
using UnityEngine;
using UnityEngine.Serialization;
using Wrappers;
using Zenject;

namespace Controllers
{
    public class AuctionCallController : MonoBehaviour
    {
        [FormerlySerializedAs("Player Seat")]
        [SerializeField]
        private Seat playerSeat;

        [FormerlySerializedAs("Level Button Group")]
        [SerializeField]
        private ToggleButtonGroup levelButtonGroup;

        [FormerlySerializedAs("Denomination Button Group")]
        [SerializeField]
        private ToggleButtonGroup denominationButtonGroup;

        private Denomination? _activeDenomination;

        private Level? _activeLevel;

        [Inject]
        private IBidFactory _bidFactory;

        [Inject]
        private ISession _session;

        private void OnEnable()
        {
            levelButtonGroup.toggled.AddListener(OnLevelToggle);
            denominationButtonGroup.toggled.AddListener(OnDenominationToggle);
        }

        private void OnDisable()
        {
            levelButtonGroup.toggled.RemoveListener(OnLevelToggle);
            denominationButtonGroup.toggled.RemoveListener(OnDenominationToggle);
        }

        private void OnLevelToggle(ToggleButton button)
        {
            if (button.Checked)
            {
                var levelWrapper = button.GetComponentInParent<LevelWrapper>();
                _activeLevel = levelWrapper.Level;
                PlayCallIfBidIsSet();
            }
            else
            {
                _activeLevel = null;
            }
        }

        private void OnDenominationToggle(ToggleButton button)
        {
            if (button.Checked)
            {
                var denominationWrapper = button.GetComponentInParent<DenominationWrapper>();
                _activeDenomination = denominationWrapper.Denomination;
                PlayCallIfBidIsSet();
            }
            else
            {
                _activeDenomination = null;
            }
        }

        private void PlayCallIfBidIsSet()
        {
            if (_activeLevel is not { } activeLevel || _activeDenomination is not { } activeDenomination)
            {
                return;
            }

            Debug.Assert(_session.Auction != null, "_session.Auction != null");
            _session.Auction.Call(_bidFactory.Create(activeLevel, activeDenomination), playerSeat);
        }
    }
}