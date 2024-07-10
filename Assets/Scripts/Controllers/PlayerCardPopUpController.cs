using Animators;
using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(PlayerCardPopUpAnimator))]
    public class PlayerCardPopUpController : CardPopUpController
    {
        protected override CardPopUpAnimator GetCardPopUpAnimator()
        {
            return GetComponent<PlayerCardPopUpAnimator>();
        }
    }
}