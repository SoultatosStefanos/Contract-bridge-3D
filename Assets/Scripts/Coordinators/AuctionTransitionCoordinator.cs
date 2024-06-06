using Events;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Coordinators
{
    public class AuctionTransitionCoordinator : MonoBehaviour
    {
        [FormerlySerializedAs("Card Arrangements Count")]
        [SerializeField]
        [Tooltip("The number of card arrangement events to wait for before emitting the transition.")]
        private int cardArrangementsCount = 4;

        private int _count;

        [Inject]
        private IEventBus _eventBus;

        private void OnEnable()
        {
            _eventBus?.On<CardsArrangedEvent>(HandleCardsArrangedEvent);
        }

        private void OnDisable()
        {
            _eventBus?.Off<CardsArrangedEvent>(HandleCardsArrangedEvent);
        }

        private void HandleCardsArrangedEvent(CardsArrangedEvent evt)
        {
            if (++_count == cardArrangementsCount)
            {
                _eventBus?.Post(new AuctionTransitionEvent());
            }
        }
    }
}