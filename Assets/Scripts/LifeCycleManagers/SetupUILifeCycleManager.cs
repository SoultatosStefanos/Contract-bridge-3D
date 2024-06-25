using Events;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace LifeCycleManagers
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
            _eventBus.On<BoardDealerSetEvent>(HandleBoardDealerSetEvent);
            _eventBus.On<DealerChipAnimatedEvent>(HandleDealerChipAnimatedEvent);
            _eventBus.On<DeckDealEvent>(HandleDeckDealEvent);
        }

        private void OnDisable()
        {
            _eventBus.Off<BoardDealerSetEvent>(HandleBoardDealerSetEvent);
            _eventBus.Off<DealerChipAnimatedEvent>(HandleDealerChipAnimatedEvent);
            _eventBus.Off<DeckDealEvent>(HandleDeckDealEvent);
        }

        private void HandleBoardDealerSetEvent(BoardDealerSetEvent evt)
        {
            assignDealerButton.SetActive(false);
        }

        private void HandleDealerChipAnimatedEvent(DealerChipAnimatedEvent evt)
        {
            shuffleAndDealButton.SetActive(true);
        }

        private void HandleDeckDealEvent(DeckDealEvent evt)
        {
            shuffleAndDealButton.SetActive(false);
        }
    }
}