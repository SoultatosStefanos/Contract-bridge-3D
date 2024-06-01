using UnityEngine;
using UnityEngine.Serialization;

// TODO Context switch controller

public class PlayerSwitchManager : MonoBehaviour
{
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

    private Camera _activeCamera;

    private Camera _eastCamera;

    private Camera _northCamera;

    private Camera _southCamera;

    private Camera _westCamera;

    private void Start()
    {
        _activeCamera = Camera.main;

        _eastCamera = east.GetComponent<Camera>();
        _northCamera = north.GetComponent<Camera>();
        _southCamera = south.GetComponent<Camera>();
        _westCamera = west.GetComponent<Camera>();
    }

    private void Update()
    {
        UpdateCameraContext();
    }

    private void UpdateCameraContext()
    {
        var cameraToSwitch = GetCameraByKey();
        if (cameraToSwitch)
        {
            ContextSwitch(cameraToSwitch);
        }

        return;

        Camera GetCameraByKey()
        {
            if (Input.GetKeyDown(northKey)) return _northCamera;
            if (Input.GetKeyDown(eastKey)) return _eastCamera;
            if (Input.GetKeyDown(southKey)) return _southCamera;
            return Input.GetKeyDown(westKey) ? _westCamera : null;
        }

        void ContextSwitch(Camera toCamera)
        {
            if (_activeCamera)
            {
                _activeCamera.enabled = false;
            }

            toCamera.enabled = true;
            _activeCamera = toCamera;
        }
    }
}