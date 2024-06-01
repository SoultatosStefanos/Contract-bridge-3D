using UnityEngine;

public class QuitManager : MonoBehaviour
{
    private const KeyCode QuitKey = KeyCode.Escape;

    private void Update()
    {
        if (Input.GetKeyDown(QuitKey))
        {
            Application.Quit();
        }
    }
}