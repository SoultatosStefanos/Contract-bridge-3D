using Events;
using Resolvers;
using UnityEngine;
using Zenject;

// TODO This will be part of the UI
namespace Controllers
{
    public class DealController : MonoBehaviour
    {
        [Inject]
        private IBoardResolver _boardResolver;

        [Inject]
        private IDeckResolver _deckResolver;

        [Inject]
        private IEventBus _eventBus;

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;

            var board = _boardResolver.GetBoard();
            var deck = _deckResolver.GetDeck();

            deck.Shuffle()
                .Deal(board);

            _eventBus.Post(new DealEvent());
        }
    }
}