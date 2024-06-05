using Events;
using Makaretu.Bridge;
using Resolvers;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

// TODO This will be part of the UI

namespace Controllers
{
    public class DealerAssignController : MonoBehaviour
    {
        [FormerlySerializedAs("Dealer Assign Key")]
        [SerializeField]
        private KeyCode dealerAssignKey = KeyCode.N;

        [FormerlySerializedAs("Dealer Seat")]
        [SerializeField]
        private Seat dealerSeat;

        [Inject]
        private IBoardResolver _boardResolver;

        [Inject]
        private IEventBus _eventBus;

        private void Update()
        {
            if (!Input.GetKeyDown(dealerAssignKey)) return;

            var board = _boardResolver.GetBoard();
            board.Dealer = dealerSeat;

            _eventBus.Post(new DealerAssignEvent());
        }
    }
}