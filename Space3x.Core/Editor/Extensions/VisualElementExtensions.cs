using UnityEngine;
using UnityEngine.UIElements;

namespace Space3x.Core.Editor.Extensions
{
    public static class VisualElementExtensions
    {
        public static T GetClosestParentOfType<T>(this VisualElement element) where T : VisualElement
        {
            VisualElement parent = element;
            while (parent != null)
            {
                if (parent is T)
                {
                    return parent as T;
                }
                parent = parent.parent;
            }
            return null;
        }
        
        public static T GetClosestParentOfType<T, TLimit>(this VisualElement element) where T : VisualElement
        {
            VisualElement parent = element;
            while (parent != null)
            {
                if (parent is T)
                    return parent as T;
                if (parent is TLimit)
                    return null;
                parent = parent.parent;
            }
            return null;
        }

        public static VisualElement GetPreviousSibling(this VisualElement element)
        {
            var index = element.parent.IndexOf(element);
            return index <= 0 ? null : element.parent.ElementAt(index - 1);
        }
        
        public static VisualElement GetPreviousSibling<TExclude>(this VisualElement element)
        {
            var index = element.parent.IndexOf(element);
            var sibling = index <= 0 ? null : element.parent.ElementAt(index - 1);
            return sibling is TExclude ? sibling.GetPreviousSibling<TExclude>() : sibling;
        }
        
        /// <summary>
        /// Add a sibling before this element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="sibling"></param>
        /// <returns></returns>
        public static VisualElement AddBefore(this VisualElement element, VisualElement sibling)
        {
            var index = Mathf.Max(element.parent.IndexOf(element), 0);
            element.parent.Insert(index, sibling);
            return element;
        }
        
        /// <summary>
        /// Add a sibling after this element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="sibling"></param>
        /// <returns></returns>
        public static VisualElement AddAfter(this VisualElement element, VisualElement sibling)
        {
            var index = Mathf.Min(
                Mathf.Max(
                    element.parent.IndexOf(element) + 1, 
                    0),
                element.parent.childCount);
            element.parent.Insert(index, sibling);
            return element;
        }
    }
}
