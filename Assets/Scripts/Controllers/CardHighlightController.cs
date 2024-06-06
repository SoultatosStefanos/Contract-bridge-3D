using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers
{
    public class CardHighlightController : MonoBehaviour
    {
        [FormerlySerializedAs("Cameras")]
        [SerializeField]
        private Camera[] cameras;

        [FormerlySerializedAs("Offset Multiplier")]
        [SerializeField]
        private float offsetMultiplier = 0.05F;

        [FormerlySerializedAs("Duration")]
        [SerializeField]
        [Tooltip("Animation duration in seconds.")]
        private float duration = 0.25f;

        private bool _isCardHighlighted;

        private Vector3 _newPosition;

        private Vector3 _originalPosition;

        private void Start()
        {
            _originalPosition = transform.position;
            _newPosition = transform.position + Vector3.up * offsetMultiplier;
        }

        private void Update()
        {
            foreach (
                var cam in cameras
                    .Where(cam => cam.gameObject.activeSelf)
                    .Where(cam => cam.enabled)
            )
            {
                CheckForHighlight(cam);
            }
        }

        private void CheckForHighlight(Camera cam)
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit)) return;

            var selection = hit.transform;

            var hitCard = selection == transform;
            if (hitCard)
            {
                if (!_isCardHighlighted)
                {
                    HighlightCard();
                }
            }
            else
            {
                if (_isCardHighlighted)
                {
                    UnhighlightCard();
                }
            }
        }

        // NOTE: Unity simply can't prove that this will be called at each frame, but it's all good.
        // ReSharper disable Unity.PerformanceAnalysis
        private void HighlightCard()
        {
            iTween.MoveTo(
                gameObject,
                iTween.Hash(
                    "position", _newPosition,
                    "time", duration,
                    "easetype", iTween.EaseType.easeInOutQuad
                )
            );

            _isCardHighlighted = true;
        }

        // NOTE: Unity simply can't prove that this will be called at each frame, but it's all good.
        // ReSharper disable Unity.PerformanceAnalysis
        private void UnhighlightCard()
        {
            iTween.MoveTo(
                gameObject,
                iTween.Hash(
                    "position", _originalPosition,
                    "time", duration,
                    "easetype", iTween.EaseType.easeInOutQuad
                )
            );

            _isCardHighlighted = false;
        }
    }
}