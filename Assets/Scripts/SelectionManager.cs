using UnityEngine;
using UnityEngine.Serialization;

public class SelectionManager : MonoBehaviour
{
    [FormerlySerializedAs("Selectable Tag")]
    [SerializeField]
    private string selectableTag = "Selectable";

    [FormerlySerializedAs("Highlight Material")]
    [SerializeField]
    private Material highlightMaterial;

    [FormerlySerializedAs("Camera")]
    [SerializeField]
    private new Camera camera;

    private Material[] _originalMaterials;

    private Transform _selection;

    private void Update()
    {
        if (_selection)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            if (selectionRenderer)
            {
                selectionRenderer.materials = _originalMaterials;
            }

            _selection = null;
        }

        var ray = camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit)) return;

        var selection = hit.transform;

        if (selection.CompareTag(selectableTag))
        {
            var selectionRenderer = selection.GetComponent<Renderer>();
            if (selectionRenderer)
            {
                _originalMaterials = selectionRenderer.materials;
                selectionRenderer.materials = new[] { highlightMaterial };
            }

            _selection = selection;
        }
    }
}