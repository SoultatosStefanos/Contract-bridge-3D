using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class GameObjectExtensions
    {
        public static IEnumerable<GameObject> FindChildrenInHierarchy(this GameObject gameObject)
        {
            var children = new List<GameObject>();
            CollectChildrenInHierarchy(gameObject.transform, children);
            return children;
        }

        private static void CollectChildrenInHierarchy(Transform parent, ICollection<GameObject> result)
        {
            foreach (Transform child in parent)
            {
                result.Add(child.gameObject);
                CollectChildrenInHierarchy(child, result);
            }
        }
    }
}