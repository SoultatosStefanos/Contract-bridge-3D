using UnityEngine;
using UnityEngine.Serialization;

public class SelectionManager : MonoBehaviour
{
    [FormerlySerializedAs("Highlight Material")]
    [SerializeField]
    private Material highlightMaterial;

    [FormerlySerializedAs("Camera")]
    [SerializeField]
    private new Camera camera;

    private Material[] _highlightMaterials;

    private bool _isHighlighted;

    private Material[] _originalMaterials;

    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _highlightMaterials = new[] { highlightMaterial };
    }

    private void Update()
    {
        if (!camera.enabled) return;

        if (!_renderer) return;

        if (_isHighlighted)
        {
            _renderer.materials = _originalMaterials;
            _isHighlighted = false;
        }

        var ray = camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit)) return;

        var selection = hit.transform;
        if (selection != transform) return;

        _originalMaterials = _renderer.materials;
        _renderer.materials = _highlightMaterials;
        _isHighlighted = true;
    }
}