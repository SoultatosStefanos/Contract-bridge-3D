using Events;
using Resolvers;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

// TODO This will be part of the UI
namespace Controllers
{
    public class DealController : MonoBehaviour
    {
        [FormerlySerializedAs("Deal Key")]
        [SerializeField]
        private KeyCode dealKey = KeyCode.M;

        [Inject]
        private IBoardResolver _boardResolver;

        [Inject]
        private IDeckResolver _deckResolver;

        [Inject]
        private IEventBus _eventBus;

        private void Update()
        {
            if (!Input.GetKeyDown(dealKey)) return;

            var board = _boardResolver.GetBoard();
            var deck = _deckResolver.GetDeck();

            deck.Shuffle()
                .Deal(board);

            _eventBus.Post(new DealEvent());
        }
    }
}