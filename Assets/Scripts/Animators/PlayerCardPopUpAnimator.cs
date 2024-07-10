using UnityEngine;

namespace Animators
{
    public class PlayerCardPopUpAnimator : CardPopUpAnimator
    {
        protected override Vector3 NewPosition()
        {
            return OriginalPosition + Vector3.up * OffsetMultiplier;
        }
    }
}