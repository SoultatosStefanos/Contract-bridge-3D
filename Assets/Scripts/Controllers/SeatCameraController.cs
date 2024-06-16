using UnityEngine;
using UnityEngine.Serialization;
using Avatar = Alteruna.Avatar;

namespace Controllers
{
    public class SeatCameraController : MonoBehaviour
    {
        private const float MaxRotation = 90.0f;

        [FormerlySerializedAs("Rotation Speed")]
        [SerializeField]
        private float rotationSpeed = 100.0f;

        [FormerlySerializedAs("Lock / Unlock Key")]
        [SerializeField]
        private KeyCode lockUnlockKey = KeyCode.L;

        private Avatar _avatar;

        private float _currentXRotation;

        private float _currentYRotation;

        private Quaternion _initialRotation;

        private void Start()
        {
            _avatar = GetComponent<Avatar>();

            if (!_avatar.IsMe)
                return;

            _initialRotation = transform.localRotation;
        }

        private void Update()
        {
            if (!_avatar.IsMe)
                return;

            if (Input.GetKeyDown(lockUnlockKey)) ToggleLockUnlock();

            if (!IsUnlocked()) return;

            var horizontalInput = Input.GetAxis("Mouse X");
            var verticalInput = Input.GetAxis("Mouse Y");

            var rotationAmountX = horizontalInput * rotationSpeed * Time.deltaTime;
            var rotationAmountY = verticalInput * rotationSpeed * Time.deltaTime;

            _currentYRotation = Mathf.Clamp(_currentYRotation + rotationAmountX, -MaxRotation, MaxRotation);
            _currentXRotation = Mathf.Clamp(_currentXRotation - rotationAmountY, -MaxRotation, MaxRotation);

            transform.localRotation = _initialRotation * Quaternion.Euler(_currentXRotation, _currentYRotation, 0);
        }

        private void OnEnable()
        {
            Unlock();
        }

        private static void ToggleLockUnlock()
        {
            if (IsUnlocked())
                Lock();
            else
                Unlock();
        }

        private static bool IsUnlocked()
        {
            return Cursor.lockState == CursorLockMode.Locked;
        }

        private static void Lock()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private static void Unlock()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}