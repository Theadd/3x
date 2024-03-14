using System;
using System.Collections.Generic;
using Space3x.Core.VirtualEntities;
using UnityEngine;

namespace Space3x.Core.Extensions
{
    public static class TransformExtensions
    {
        public static Transform FindRecursive(this Transform self, string exactName) => self.FindRecursive(child => child.name == exactName);

        public static Transform FindRecursive(this Transform self, Func<Transform, bool> selector)
        {
            foreach (Transform child in self)
            {
                if (selector(child))
                {
                    return child;
                }

                var finding = child.FindRecursive(selector);

                if (finding != null)
                {
                    return finding;
                }
            }

            return null;
        }
        
        /// <summary>
        /// Like TransformPoint, InverseTransformPoint, etc. but for rotation.
        /// </summary>
        public static Quaternion TransformRotation(this Transform self, Quaternion localRotation) => self.rotation * localRotation;
        public static Quaternion InverseTransformRotation(this Transform self, Quaternion worldRotation) => Quaternion.Inverse(self.rotation) * worldRotation;
        
        public static IEnumerable<Transform> GetChildren(this Transform parent)
        {
            for (int Idx = 0; Idx < parent.childCount; ++Idx)
            {
                yield return parent.GetChild(Idx);
            }
        }

        /// <summary>
        /// Search upward through the transform hierarchy and return the first T it finds.
        /// </summary>
        public static T GetComponentInAncestors<T>(this Transform obj) where T : class
        {
            while (obj != null)
            {
                T result = obj.GetComponent<T>();
                if (result != null)
                    return result;
                obj = obj.parent;
            }
            return null;
        }
        
        /// <summary>
        /// Search upward through the transform hierarchy and return the first T it find which is also of type T2.
        /// </summary>
        public static T2 GetComponentInAncestors<T, T2>(this Transform self) where T2: class
        {
            while (self != null)
            {
                if (self.GetComponent<T>() is T2 result) return result;
                self = self.parent;
            }
            return null;
        }
        
        public static EntityProvider<T> LocalProvider<T>(this Transform self) where T : class => 
            self.GetComponentInAncestors<IEntityProvider, T>() as EntityProvider<T>;
    }
}
