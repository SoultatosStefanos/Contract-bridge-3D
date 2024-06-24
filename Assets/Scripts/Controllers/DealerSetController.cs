using ContractBridge.Core;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Controllers
{
    public class DealerSetController : MonoBehaviour
    {
        [FormerlySerializedAs("Dealer Seat")]
        [SerializeField]
        private Seat dealerSeat;

        [Inject]
        private IBoard _board;

        public void SetDealer()
        {
            _board.Dealer = dealerSeat;
        }
    }
}