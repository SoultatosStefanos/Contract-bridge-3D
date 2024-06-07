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

        [FormerlySerializedAs("Ignore for Hidden Cursor.")]
        [SerializeField]
        private bool ignoreForHiddenCursor = true;

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
            var activeCamera = cameras.First(cam => cam.gameObject.activeSelf);
            CheckForCardHighlight(activeCamera);
        }

        private void CheckForCardHighlight(Camera cam)
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
            if (ignoreForHiddenCursor && IsCursorHidden()) return; // Ignore when looking around.

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

        private static bool IsCursorHidden()
        {
            return Cursor.lockState == CursorLockMode.Locked;
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