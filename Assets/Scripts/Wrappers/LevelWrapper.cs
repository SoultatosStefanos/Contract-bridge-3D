using ContractBridge.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Wrappers
{
    public class LevelWrapper : MonoBehaviour
    {
        [FormerlySerializedAs("Level")]
        [SerializeField]
        private Level level;

        public Level Level => level;
    }
}