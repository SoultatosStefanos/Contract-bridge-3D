using Buttons;
using ContractBridge.Core;
using Events;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Presenters
{
    [RequireComponent(typeof(ToggleButton))]
    public class AuctionCallActionPresenter : MonoBehaviour
    {
        [FormerlySerializedAs("Player Seat")]
        [SerializeField]
        private Seat playerSeat;

        [Inject]
        private IEventBus _eventBus;

        private ToggleButton _toggleButton;

        private void Start()
        {
            _toggleButton = GetComponent<ToggleButton>();
        }

        private void OnEnable()
        {
            _eventBus.On<AuctionCallEvent>(HandleAuctionCallEvent);
        }

        private void OnDisable()
        {
            _eventBus.Off<AuctionCallEvent>(HandleAuctionCallEvent);
        }

        private void HandleAuctionCallEvent(AuctionCallEvent e)
        {
            var isPlayerCall = e.Seat == playerSeat;
            if (isPlayerCall)
            {
                _toggleButton.Checked = false;
            }
        }
    }
}