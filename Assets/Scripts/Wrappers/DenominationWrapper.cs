using ContractBridge.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Wrappers
{
    public class DenominationWrapper : MonoBehaviour
    {
        [FormerlySerializedAs("Denomination")]
        [SerializeField]
        private Denomination denomination;

        public Denomination Denomination => denomination;
    }
}