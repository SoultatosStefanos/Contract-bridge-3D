using ContractBridge.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Wrappers
{
    public class SeatWrapper : MonoBehaviour
    {
        [FormerlySerializedAs("Seat")]
        [SerializeField]
        private Seat seat;

        public Seat Seat => seat;
    }
}