using UnityEngine;
using UnityEngine.Serialization;

public class QuitManager : MonoBehaviour
{
    [FormerlySerializedAs("Quit Key")] [SerializeField]
    private KeyCode quitKey = KeyCode.Escape;

    private void Update()
    {
        if (Input.GetKeyDown(quitKey))
        {
            Application.Quit();
        }
    }
}