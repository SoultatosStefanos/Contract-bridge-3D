using UnityEngine;
using UnityEngine.Serialization;

namespace Animators
{
    public class CameraSnapAnimator : MonoBehaviour
    {
        [FormerlySerializedAs("Duration")]
        [SerializeField]
        private float duration = 0.5F;

        public void SnapAnimate(GameObject cameraGameObject, Transform snapTransform)
        {
            iTween.MoveTo(
                cameraGameObject,
                iTween.Hash(
                    "position", snapTransform.position,
                    "time", duration,
                    "easetype", iTween.EaseType.easeInOutSine
                )
            );

            iTween.RotateTo(
                cameraGameObject,
                iTween.Hash(
                    "rotation", snapTransform.rotation.eulerAngles,
                    "time", duration,
                    "easetype", iTween.EaseType.easeInOutSine
                )
            );
        }

        public void SnapBackAnimate(GameObject cameraGameObject, Vector3 originalPosition, Quaternion originalRotation)
        {
            iTween.MoveTo(
                cameraGameObject,
                iTween.Hash(
                    "position", originalPosition,
                    "time", duration,
                    "easetype", iTween.EaseType.easeInOutSine
                )
            );

            iTween.RotateTo(
                cameraGameObject,
                iTween.Hash(
                    "rotation", originalRotation.eulerAngles,
                    "time", duration,
                    "easetype", iTween.EaseType.easeInOutSine
                )
            );
        }
    }
}