using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers
{
    // TODO Remove (Kept for testing only).
    public class ContextSwitchController : MonoBehaviour
    {
        [FormerlySerializedAs("Main")]
        [SerializeField]
        private GameObject main;

        [FormerlySerializedAs("North")]
        [SerializeField]
        private GameObject north;

        [FormerlySerializedAs("East")]
        [SerializeField]
        private GameObject east;

        [FormerlySerializedAs("South")]
        [SerializeField]
        private GameObject south;

        [FormerlySerializedAs("West")]
        [SerializeField]
        private GameObject west;

        [FormerlySerializedAs("Main Key")]
        [SerializeField]
        private KeyCode mainKey = KeyCode.Alpha0;

        [FormerlySerializedAs("East Key")]
        [SerializeField]
        private KeyCode eastKey = KeyCode.Alpha2;

        [FormerlySerializedAs("North Key")]
        [SerializeField]
        private KeyCode northKey = KeyCode.Alpha1;

        [FormerlySerializedAs("South Key")]
        [SerializeField]
        private KeyCode southKey = KeyCode.Alpha3;

        [FormerlySerializedAs("West Key")]
        [SerializeField]
        private KeyCode westKey = KeyCode.Alpha4;

        private GameObject _currContext;

        private void Start()
        {
            _currContext = main;
        }

        private void Update()
        {
            var newCtx = GetContextToSwitch();
            if (newCtx) ContextSwitch(newCtx);

            return;

            GameObject GetContextToSwitch()
            {
                if (Input.GetKeyDown(northKey)) return north;
                if (Input.GetKeyDown(eastKey)) return east;
                if (Input.GetKeyDown(southKey)) return south;
                if (Input.GetKeyDown(westKey)) return west;
                return Input.GetKeyDown(mainKey) ? main : null;
            }

            void ContextSwitch(GameObject ctx)
            {
                if (_currContext) _currContext.SetActive(false);

                ctx.SetActive(true);
                _currContext = ctx;
            }
        }
    }
}