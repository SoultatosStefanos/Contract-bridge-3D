using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers
{
    public class QuitController : MonoBehaviour
    {
        [FormerlySerializedAs("Quit Key")]
        [SerializeField]
        private KeyCode quitKey = KeyCode.Escape;

        private void Update()
        {
            if (Input.GetKeyDown(quitKey)) Application.Quit();
        }
    }
}