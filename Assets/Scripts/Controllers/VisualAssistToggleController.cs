using System;
using ContractBridge.Core;
using Presenters;
using Registries;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Controllers
{
    public class VisualAssistToggleController : MonoBehaviour
    {
        [FormerlySerializedAs("Toggle Key Code")]
        [SerializeField]
        private KeyCode toggleKeyCode = KeyCode.H;

        [FormerlySerializedAs("Player Seat")]
        [SerializeField]
        private Seat playerSeat;

        // NOTE: Could/Should also use tags here, but whatever.

        [FormerlySerializedAs("Makeable Contracts Panel")]
        [SerializeField]
        private GameObject makeAbleContractsPanel;

        [FormerlySerializedAs("Player Card Highlight Presenter")]
        [SerializeField]
        private CardHighlightPresenter playerCardHighlightPresenter;

        [FormerlySerializedAs("Partner Card Highlight Presenter")]
        [SerializeField]
        private CardHighlightPresenter partnerCardHighlightPresenter;

        [Inject]
        private ICardGameObjectRegistry _cardGameObjectRegistry;

        [Inject]
        private ISession _session;

        private void Update()
        {
            if (Input.GetKeyDown(toggleKeyCode))
            {
                ToggleVisualAssist();
            }
        }

        private void ToggleVisualAssist()
        {
            switch (_session.Phase)
            {
                case Phase.Auction:
                    ToggleAuctionVisualAssist();
                    break;

                case Phase.Play:
                    TogglePlayVisualAssist();
                    break;

                case Phase.Setup:
                case Phase.Scoring:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ToggleAuctionVisualAssist()
        {
            makeAbleContractsPanel.SetActive(!makeAbleContractsPanel.activeSelf);
        }

        private void TogglePlayVisualAssist()
        {
            playerCardHighlightPresenter.enabled = !playerCardHighlightPresenter.enabled;

            if (DummySeat() is { } dummySeat && dummySeat == playerSeat.Partner())
            {
                partnerCardHighlightPresenter.enabled = !partnerCardHighlightPresenter.enabled;
            }
        }

        private Seat? DummySeat()
        {
            return _session.Auction?.FinalContract?.Dummy();
        }
    }
}