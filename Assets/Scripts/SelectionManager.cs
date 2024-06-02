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
        _highlightMaterials = new[] { highlightMaterial };
        _renderer = GetComponent<Renderer>();
        if (_renderer)
        {
            _originalMaterials = _renderer.materials;
        }
        else
        {
            Debug.LogWarning($"No Renderer attached to {name}");
        }
    }

    private void Update()
    {
        if (!camera.enabled || !_renderer) return;

        HandleSelection();
    }

    private void HandleSelection()
    {
        if (_isHighlighted)
        {
            UnhighlightObject();
        }

        var ray = camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit)) return;

        var selection = hit.transform;
        if (selection != transform) return;

        if (!_isHighlighted)
        {
            HighlightObject();
        }
    }

    private void HighlightObject()
    {
        _renderer.materials = _highlightMaterials;
        _isHighlighted = true;
    }

    private void UnhighlightObject()
    {
        _renderer.materials = _originalMaterials;
        _isHighlighted = false;
    }
}