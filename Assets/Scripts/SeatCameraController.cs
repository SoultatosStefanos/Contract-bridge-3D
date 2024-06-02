using UnityEngine;
using UnityEngine.Serialization;

public class SeatCameraController : MonoBehaviour
{
    private const float MaxRotation = 90.0f;

    [FormerlySerializedAs("Rotation Speed")]
    [SerializeField]
    private float rotationSpeed = 100.0f;

    private float _currentXRotation;

    private float _currentYRotation;

    private Quaternion _initialRotation;

    private void Start()
    {
        _initialRotation = transform.localRotation;
    }

    private void Update()
    {
        var horizontalInput = Input.GetAxis("Mouse X");
        var verticalInput = Input.GetAxis("Mouse Y");

        var rotationAmountX = horizontalInput * rotationSpeed * Time.deltaTime;
        var rotationAmountY = verticalInput * rotationSpeed * Time.deltaTime;

        _currentYRotation = Mathf.Clamp(_currentYRotation + rotationAmountX, -MaxRotation, MaxRotation);
        _currentXRotation = Mathf.Clamp(_currentXRotation - rotationAmountY, -MaxRotation, MaxRotation);

        transform.localRotation = _initialRotation * Quaternion.Euler(_currentXRotation, _currentYRotation, 0);
    }
}