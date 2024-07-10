using UnityEngine;
using UnityEngine.Serialization;

namespace Animators
{
    public class CardPopUpAnimator : MonoBehaviour
    {
        [FormerlySerializedAs("Offset Multiplier")]
        [SerializeField]
        private float offsetMultiplier = 0.05F;

        [FormerlySerializedAs("Duration")]
        [SerializeField]
        [Tooltip("Animation duration in seconds.")]
        private float duration = 0.25f;

        private Vector3 _newPosition;

        private Vector3 _originalPosition = Vector3.zero;

        public void PopUpCard()
        {
            if (_originalPosition == Vector3.zero)
            {
                _originalPosition = transform.position;
                _newPosition = transform.position + Vector3.up * offsetMultiplier;
            }

            iTween.MoveTo(
                gameObject,
                iTween.Hash(
                    "position", _newPosition,
                    "time", duration,
                    "easetype", iTween.EaseType.easeInOutQuad
                )
            );
        }

        public void UndoCardPopUp()
        {
            iTween.MoveTo(
                gameObject,
                iTween.Hash(
                    "position", _originalPosition,
                    "time", duration,
                    "easetype", iTween.EaseType.easeInOutQuad
                )
            );
        }
    }
}