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

        [FormerlySerializedAs("Snap Mouse Button Index")]
        [SerializeField]
        [Tooltip("Left: 0, Right: 1, Middle: 2")]
        private int snapMouseButtonIndex = 1;

        [FormerlySerializedAs("Snap Transform")]
        [SerializeField]
        private Transform snapTransform;

        [FormerlySerializedAs("Camera Snap Animator")]
        [SerializeField]
        private CameraSnapAnimator cameraSnapAnimator;

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
            if (Input.GetMouseButtonDown(snapMouseButtonIndex))
            {
                Snap();
            }

            if (Input.GetMouseButtonUp(snapMouseButtonIndex))
            {
                SnapBack();
            }

            if (IsSnapped())
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

        private bool IsSnapped()
        {
            return transform.position == snapTransform.position && transform.rotation == snapTransform.rotation;
        }

        // NOTE: This is ok, it won't be called at each frame.
        // ReSharper disable Unity.PerformanceAnalysis
        private void Snap()
        {
            _originalPosition = transform.position;
            _originalRotation = transform.rotation;

            cameraSnapAnimator.SnapAnimate(gameObject, snapTransform);
        }

        // NOTE: This is ok, it won't be called at each frame.
        // ReSharper disable Unity.PerformanceAnalysis
        private void SnapBack()
        {
            cameraSnapAnimator.SnapBackAnimate(gameObject, _originalPosition, _originalRotation);
        }
    }
}