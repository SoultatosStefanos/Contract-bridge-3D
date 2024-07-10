using UnityEngine;
using UnityEngine.Serialization;

namespace Animators
{
    public abstract class CardPopUpAnimator : MonoBehaviour
    {
        [FormerlySerializedAs("Offset Multiplier")]
        [SerializeField]
        private float offsetMultiplier = 0.05F;

        [FormerlySerializedAs("Duration")]
        [SerializeField]
        [Tooltip("Animation duration in seconds.")]
        private float duration = 0.25f;

        private Vector3 _newPosition;

        protected Vector3 OriginalPosition { get; private set; }

        protected float OffsetMultiplier => offsetMultiplier;

        private void Start()
        {
            OriginalPosition = transform.position;
            _newPosition = NewPosition();
        }

        protected abstract Vector3 NewPosition();

        public void PopUpCard()
        {
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
                    "position", OriginalPosition,
                    "time", duration,
                    "easetype", iTween.EaseType.easeInOutQuad
                )
            );
        }
    }
}