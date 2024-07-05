using System.Collections.Generic;
using System.Linq;
using Components;
using ContractBridge.Core;
using ContractBridge.Core.Impl;
using Events;
using Extensions;
using Groups;
using UnityEngine;
using UnityEngine.Serialization;
using Wrappers;
using Zenject;

namespace Presenters
{
    public class AuctionCallPresenter : MonoBehaviour
    {
        private const string BiddingActionsLevelButtonTag = "Bidding Actions / Level Button";

        private const string BiddingActionsDenominationButtonTag = "Bidding Actions / Denomination Button";

        [FormerlySerializedAs("Player Seat")]
        [SerializeField]
        private Seat playerSeat;

        [FormerlySerializedAs("Level Button Group")]
        [SerializeField]
        private ToggleButtonGroup levelButtonGroup;

        [Inject]
        private IBidFactory _bidFactory;

        private ICollection<GameObject> _denominationButtons;

        [Inject]
        private IEventBus _eventBus;

        private ICollection<GameObject> _levelButtons;

        [Inject]
        private ISession _session;

        private void Awake()
        {
            _levelButtons = gameObject.FindChildrenInHierarchy()
                .Where(child => child.CompareTag(BiddingActionsLevelButtonTag))
                .ToList();

            _denominationButtons = gameObject.FindChildrenInHierarchy()
                .Where(child => child.CompareTag(BiddingActionsDenominationButtonTag))
                .ToList();
        }

        public void OnEnable()
        {
            _eventBus.On<AuctionTurnChangeEvent>(HandleAuctionTurnChangeEvent);
            _eventBus.On<AuctionFinalContractEvent>(HandleAuctionFinalContractEvent);
            _eventBus.On<AuctionCallEvent>(HandleAuctionCallEvent);

            levelButtonGroup.toggled.AddListener(OnLevelToggled);
        }

        private void OnDisable()
        {
            _eventBus.Off<AuctionTurnChangeEvent>(HandleAuctionTurnChangeEvent);
            _eventBus.Off<AuctionFinalContractEvent>(HandleAuctionFinalContractEvent);
            _eventBus.Off<AuctionCallEvent>(HandleAuctionCallEvent);

            levelButtonGroup.toggled.RemoveListener(OnLevelToggled);
        }

        private void HandleAuctionTurnChangeEvent(AuctionTurnChangeEvent e)
        {
            UpdateVisualOnTurn();
        }

        private void HandleAuctionFinalContractEvent(AuctionFinalContractEvent e)
        {
            UpdateVisualOnTurn();
        }

        private void UpdateVisualOnTurn()
        {
            ShowPlayableLevelButtons();
            HideAllDenominationButtons();
        }

        private IAuction Auction()
        {
            Debug.Assert(_session.Auction != null, "_session.Auction != null");
            return _session.Auction;
        }

        private void ShowPlayableLevelButtons()
        {
            foreach (var button in _levelButtons)
            {
                var buttonLevelWrapper = button.GetComponent<LevelWrapper>();
                var buttonLevel = buttonLevelWrapper.Level;

                button.SetActive(
                    EnumExtensions.AllValues<Denomination>().Any(
                        denomination => Auction().CanCall(
                            _bidFactory.Create(buttonLevel, denomination),
                            playerSeat
                        )
                    )
                );
            }
        }

        private void HideAllDenominationButtons()
        {
            foreach (var button in _denominationButtons)
            {
                button.SetActive(false);
            }
        }

        private void OnLevelToggled(ToggleButton button)
        {
            if (button.Checked)
            {
                var buttonLevelWrapper = button.GetComponentInParent<LevelWrapper>();
                var buttonLevel = buttonLevelWrapper.Level;

                ShowPlayableDenominationButtons(buttonLevel);
            }
            else
            {
                HideAllDenominationButtons();
            }
        }

        private void ShowPlayableDenominationButtons(Level level)
        {
            foreach (var button in _denominationButtons)
            {
                var buttonDenominationWrapper = button.GetComponent<DenominationWrapper>();
                var buttonDenomination = buttonDenominationWrapper.Denomination;

                button.SetActive(
                    Auction().CanCall(
                        _bidFactory.Create(level, buttonDenomination),
                        playerSeat
                    )
                );
            }
        }

        private void HandleAuctionCallEvent(AuctionCallEvent e)
        {
            if (e.Seat == playerSeat)
            {
                UncheckCallButtons();
            }
        }

        private void UncheckCallButtons()
        {
            foreach (var button in _levelButtons.Concat(_denominationButtons))
            {
                var toggleButton = button.GetComponent<ToggleButton>();
                toggleButton.Checked = false;
            }
        }
    }
}