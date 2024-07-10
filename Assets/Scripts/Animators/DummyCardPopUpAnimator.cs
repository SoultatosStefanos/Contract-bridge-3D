using UnityEngine;

namespace Animators
{
    public class DummyCardPopUpAnimator : CardPopUpAnimator
    {
        protected override Vector3 NewPosition()
        {
            return OriginalPosition + Vector3.right * OffsetMultiplier;
        }
    }
}