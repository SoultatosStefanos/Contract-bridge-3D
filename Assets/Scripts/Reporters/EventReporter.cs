using Events;
using UnityEngine;
using Zenject;

namespace Reporters
{
    public class EventReporter<TEvent> : MonoBehaviour
    {
        [Inject]
        private IEventBus _eventBus;

        private void OnEnable()
        {
            _eventBus?.On<TEvent>(HandleEvent);
        }

        private void OnDisable()
        {
            _eventBus?.Off<TEvent>(HandleEvent);
        }

        private void HandleEvent(TEvent evt)
        {
            Debug.Log($"Event: {evt}");
        }
    }
}