using Animators;
using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(DummyCardPopUpAnimator))]
    public class DummyCardPopUpController : CardPopUpController
    {
        protected override CardPopUpAnimator GetCardPopUpAnimator()
        {
            return GetComponent<DummyCardPopUpAnimator>();
        }
    }
}