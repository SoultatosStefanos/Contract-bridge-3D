using Events;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace LifeCycle
{
    public class SetupUILifeCycleManager : MonoBehaviour
    {
        [FormerlySerializedAs("Assign Dealer Button")]
        [SerializeField]
        private GameObject assignDealerButton;

        [FormerlySerializedAs("Shuffle & Deal Button")]
        [SerializeField]
        private GameObject shuffleAndDealButton;

        [Inject]
        private IEventBus _eventBus;

        private void OnEnable()
        {
            _eventBus?.On<DealerAssignEvent>(HandleDealerAssignEvent);
            _eventBus?.On<DealerChipArrangedEvent>(HandleDealerChipArrangedEvent);
            _eventBus?.On<DealEvent>(HandleDealEvent);
        }

        private void OnDisable()
        {
            _eventBus?.Off<DealerAssignEvent>(HandleDealerAssignEvent);
            _eventBus?.Off<DealerChipArrangedEvent>(HandleDealerChipArrangedEvent);
            _eventBus?.Off<DealEvent>(HandleDealEvent);
        }

        private void HandleDealerAssignEvent(DealerAssignEvent evt)
        {
            assignDealerButton?.SetActive(false);
        }

        private void HandleDealerChipArrangedEvent(DealerChipArrangedEvent evt)
        {
            shuffleAndDealButton?.SetActive(true);
        }

        private void HandleDealEvent(DealEvent evt)
        {
            shuffleAndDealButton?.SetActive(false);
        }
    }
}