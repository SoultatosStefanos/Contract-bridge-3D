using UnityEngine;
using UnityEngine.Serialization;

public class CursorController : MonoBehaviour
{
    [FormerlySerializedAs("Lock / Unlock Key")]
    [SerializeField]
    private KeyCode lockUnlockKey = KeyCode.L;

    private void Update()
    {
        if (Input.GetKeyDown(lockUnlockKey))
        {
            ToggleShowHideCursor();
        }
    }

    private static void ToggleShowHideCursor()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            ShowCursor();
        }
        else
        {
            HideCursor();
        }
    }

    private static void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private static void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}