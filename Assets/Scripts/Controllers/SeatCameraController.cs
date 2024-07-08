using Animators;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers
{
    public class SeatCameraController : MonoBehaviour
    {
        private const float MaxRotation = 90.0f;

        [FormerlySerializedAs("Rotation Speed")]
        [SerializeField]
        private float rotationSpeed = 100.0f;

        [FormerlySerializedAs("Lock / Unlock Mouse Button Index")]
        [SerializeField]
        [Tooltip("Left: 0, Right: 1, Middle: 2")]
        private int lockUnlockMouseButtonIndex = 2;

        [FormerlySerializedAs("Half Stand Mouse Button Index")]
        [SerializeField]
        [Tooltip("Left: 0, Right: 1, Middle: 2")]
        private int halfStandMouseButtonIndex = 1;

        [FormerlySerializedAs("Half Stand Transform")]
        [SerializeField]
        private Transform halfStandTransform;

        [FormerlySerializedAs("Camera Half Stand Animator")]
        [SerializeField]
        private CameraHalfStandAnimator cameraHalfStandAnimator;

        private float _currentXRotation;

        private float _currentYRotation;

        private Quaternion _initialRotation;

        private Vector3 _originalPosition;

        private Quaternion _originalRotation;

        private void Awake()
        {
            _initialRotation = transform.localRotation;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(halfStandMouseButtonIndex))
            {
                HalfStand();
            }

            if (Input.GetMouseButtonUp(halfStandMouseButtonIndex))
            {
                SitDown();
            }

            if (IsHalfStanding())
            {
                return;
            }

            if (Input.GetMouseButtonDown(lockUnlockMouseButtonIndex))
            {
                Unlock();
            }

            if (Input.GetMouseButtonUp(lockUnlockMouseButtonIndex))
            {
                Lock();
            }

            if (!IsUnlocked())
            {
                return;
            }

            RotateCamera();
        }

        private void OnEnable()
        {
            Lock();
        }

        private void RotateCamera()
        {
            var horizontalInput = Input.GetAxis("Mouse X");
            var verticalInput = Input.GetAxis("Mouse Y");

            var rotationAmountX = horizontalInput * rotationSpeed * Time.deltaTime;
            var rotationAmountY = verticalInput * rotationSpeed * Time.deltaTime;

            _currentYRotation = Mathf.Clamp(_currentYRotation + rotationAmountX, -MaxRotation, MaxRotation);
            _currentXRotation = Mathf.Clamp(_currentXRotation - rotationAmountY, -MaxRotation, MaxRotation);

            transform.localRotation = _initialRotation * Quaternion.Euler(_currentXRotation, _currentYRotation, 0);
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

        private bool IsHalfStanding()
        {
            return transform.position == halfStandTransform.position &&
                   transform.rotation == halfStandTransform.rotation;
        }

        // NOTE: This is ok, it won't be called at each frame.
        // ReSharper disable Unity.PerformanceAnalysis
        private void HalfStand()
        {
            _originalPosition = transform.position;
            _originalRotation = transform.rotation;

            cameraHalfStandAnimator.HalfStand(gameObject, halfStandTransform);
        }

        // NOTE: This is ok, it won't be called at each frame.
        // ReSharper disable Unity.PerformanceAnalysis
        private void SitDown()
        {
            cameraHalfStandAnimator.SitDown(gameObject, _originalPosition, _originalRotation);
        }
    }
}