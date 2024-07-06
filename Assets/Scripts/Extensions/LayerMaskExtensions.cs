using UnityEngine;

namespace Extensions
{
    public static class LayerMaskExtensions
    {
        public static int LayerNumber(this LayerMask mask)
        {
            return (int)Mathf.Log(mask.value, 2); // WARNING: Use with caution...
        }
    }
}