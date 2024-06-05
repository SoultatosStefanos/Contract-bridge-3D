using Events;
using Makaretu.Bridge;
using Resolvers;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Handlers
{
    public class DealerAssignHandler : MonoBehaviour
    {
        [FormerlySerializedAs("Dealer Seat")]
        [SerializeField]
        private Seat dealerSeat;

        [Inject]
        private IBoardResolver _boardResolver;

        [Inject]
        private IEventBus _eventBus;

        public void Invoke()
        {
            var board = _boardResolver.GetBoard();
            board.Dealer = dealerSeat;

            _eventBus.Post(new DealerAssignEvent());
        }
    }
}