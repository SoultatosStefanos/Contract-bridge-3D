using Events;
using Registries;
using UnityEngine;
using Zenject;

namespace Animators
{
    public class CardFollowAnimator : MonoBehaviour
    {
        [Inject]
        private ICardGameObjectRegistry _cardGameObjectRegistry;

        [Inject]
        private IEventBus _eventBus;

        private void OnEnable()
        {
            _eventBus.On<GameFollowEvent>(HandleGameFollowEvent);
        }

        private void OnDisable()
        {
            _eventBus.Off<GameFollowEvent>(HandleGameFollowEvent);
        }

        private void HandleGameFollowEvent(GameFollowEvent e)
        {
            var cardGameObject = _cardGameObjectRegistry.GetGameObject(e.Card);
            Debug.Log($"Will animate: {cardGameObject.name}");

            // TODO
        }
    }
}