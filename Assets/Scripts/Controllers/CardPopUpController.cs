using System.Linq;
using Animators;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers
{
    public abstract class CardPopUpController : MonoBehaviour
    {
        [FormerlySerializedAs("Cameras")]
        [SerializeField]
        private Camera[] cameras;

        [FormerlySerializedAs("Ignore for Hidden Cursor.")]
        [SerializeField]
        private bool ignoreForHiddenCursor = true;

        private bool _isCardPoppedUp;

        private CardPopUpAnimator _popUpAnimator;

        private void Awake()
        {
            _popUpAnimator = GetCardPopUpAnimator();
        }

        private void Update()
        {
            var activeCamera = cameras.First(cam => cam.gameObject.activeSelf);
            CheckForCardPopUp(activeCamera);
        }

        protected abstract CardPopUpAnimator GetCardPopUpAnimator();

        private void CheckForCardPopUp(Camera cam)
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit)) return;

            var selection = hit.transform;

            var hitCard = selection == transform;
            if (hitCard)
            {
                if (!_isCardPoppedUp)
                {
                    PopUpCard();
                }
            }
            else
            {
                if (_isCardPoppedUp)
                {
                    UndoCardPopUp();
                }
            }
        }

        private static bool IsCursorHidden()
        {
            return Cursor.lockState == CursorLockMode.Locked;
        }

        // NOTE: Unity simply can't prove that this will be called at each frame, but it's all good.
        // ReSharper disable Unity.PerformanceAnalysis
        private void PopUpCard()
        {
            if (ignoreForHiddenCursor && IsCursorHidden()) // Ignore when looking around.
            {
                return;
            }

            _popUpAnimator.PopUpCard();

            _isCardPoppedUp = true;
        }

        // NOTE: Unity simply can't prove that this will be called at each frame, but it's all good.
        // ReSharper disable Unity.PerformanceAnalysis
        private void UndoCardPopUp()
        {
            _popUpAnimator.UndoCardPopUp();

            _isCardPoppedUp = false;
        }
    }
}