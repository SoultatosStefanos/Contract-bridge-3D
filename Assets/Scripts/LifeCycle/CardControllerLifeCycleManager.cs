using Controllers;
using Events;
using Mappers;
using Resolvers;
using UnityEngine;
using Zenject;

namespace LifeCycle
{
    public class CardControllerLifeCycleManager : MonoBehaviour
    {
        [Inject]
        private ICardMapper _cardMapper;

        [Inject]
        private IDeckResolver _deckResolver;

        [Inject]
        private IEventBus _eventBus;

        private void OnEnable()
        {
            _eventBus?.On<AuctionTransitionEvent>(HandleAuctionTransitionEvent);
        }

        private void OnDisable()
        {
            _eventBus?.Off<AuctionTransitionEvent>(HandleAuctionTransitionEvent);
        }

        private void HandleAuctionTransitionEvent(AuctionTransitionEvent evt)
        {
            var deck = _deckResolver.GetDeck();

            foreach (var card in deck.Cards)
            {
                var cardGameObject = _cardMapper.GetGameObject(card);

                var cardHighlightComponent = cardGameObject.GetComponent<CardHighlightController>();
                cardHighlightComponent.enabled = true;
            }
        }
    }
}