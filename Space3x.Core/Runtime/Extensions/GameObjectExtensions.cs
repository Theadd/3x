using UnityEngine;

namespace Space3x.Core.Extensions
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Just like the <c>copy -> paste component as new</c> feature from the Inspector tab.
        /// Usage:
        /// <example>
        /// <code>
        /// Health myHealth = gameObject.AddCopyAsNewComponent&lt;Health&gt;(enemy.health);
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="self"></param>
        /// <param name="componentToCopyValuesFrom"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>The newly created component.</returns>
        public static T AddCopyAsNewComponent<T>(this GameObject self, T componentToCopyValuesFrom) where T : Component
        {
            return self.AddComponent<T>().GetCopyOf(componentToCopyValuesFrom) as T;
        }

        public static void SetLayer(this GameObject self, int layerIndex, bool applyRecursivelyOnChildren = false)
        {
            self.layer = layerIndex;
            if (!applyRecursivelyOnChildren) return;
            for (var i = 0; i < self.transform.childCount; ++i)
            {
                self.transform.GetChild(i).gameObject.SetLayer(layerIndex, true);
            }
        }
    }
}
