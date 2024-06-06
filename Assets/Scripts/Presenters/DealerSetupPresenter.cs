using Events;
using Makaretu.Bridge;
using TMPro;
using UnityEngine;
using Zenject;

namespace Presenters
{
    public class DealerSetupPresenter : MonoBehaviour
    {
        private TextMeshProUGUI _dealerText;

        [Inject]
        private IEventBus _eventBus;

        private void Start()
        {
            _dealerText = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _eventBus?.On<DealerAssignEvent>(HandleDealerAssignEvent);
        }

        private void OnDisable()
        {
            _eventBus?.Off<DealerAssignEvent>(HandleDealerAssignEvent);
        }

        private void HandleDealerAssignEvent(DealerAssignEvent evt)
        {
            UpdateDealerText(evt.Seat);
        }

        private void UpdateDealerText(Seat dealerSeat)
        {
            _dealerText.text = $"Dealer: {dealerSeat}";
        }
    }
}