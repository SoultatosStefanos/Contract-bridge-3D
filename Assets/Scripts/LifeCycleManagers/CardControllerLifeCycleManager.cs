using ContractBridge.Core;
using Controllers;
using Events;
using Registries;
using UnityEngine;
using Zenject;

namespace LifeCycleManagers
{
    public class CardControllerLifeCycleManager : MonoBehaviour
    {
        [Inject]
        private ICardGameObjectRegistry _cardGameObjectRegistry;

        [Inject]
        private IDeck _deck;

        [Inject]
        private IEventBus _eventBus;

        private void OnEnable()
        {
            _eventBus.On<SessionPhaseChangedEvent>(HandleSessionPhaseChangedEvent);
        }

        private void OnDisable()
        {
            _eventBus.Off<SessionPhaseChangedEvent>(HandleSessionPhaseChangedEvent);
        }

        private void HandleSessionPhaseChangedEvent(SessionPhaseChangedEvent evt)
        {
            if (evt.Phase != Phase.Auction)
            {
                return;
            }

            foreach (var card in _deck)
            {
                var cardGameObject = _cardGameObjectRegistry.GetGameObject(card);

                var cardHighlightComponent = cardGameObject.GetComponent<CardHighlightController>();
                cardHighlightComponent.enabled = true;
            }
        }
    }
}