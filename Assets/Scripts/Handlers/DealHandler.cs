using Events;
using Resolvers;
using UnityEngine;
using Zenject;

namespace Handlers
{
    public class DealHandler : MonoBehaviour
    {
        [Inject]
        private IBoardResolver _boardResolver;

        [Inject]
        private IDeckResolver _deckResolver;

        [Inject]
        private IEventBus _eventBus;

        public void Invoke()
        {
            var board = _boardResolver.GetBoard();
            var deck = _deckResolver.GetDeck();

            deck.Shuffle()
                .Deal(board);

            _eventBus?.Post(new ShuffleEvent());
            _eventBus?.Post(new DealEvent());
        }
    }
}