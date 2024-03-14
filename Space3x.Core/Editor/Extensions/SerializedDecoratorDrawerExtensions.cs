using System;
using Space3x.Core.Editor.Attributes.Drawers;
using Space3x.Core.Editor.Attributes.VisualElements;
using Space3x.Core.Editor.Utilities;
using UnityEngine.UIElements;

namespace Space3x.Core.Editor.Extensions
{
    public static class SerializedDecoratorDrawerExtensions
    {
        public static bool TryGetConditionValue<T>(this SerializedDecoratorDrawer<T> self, string conditionPropertyName, out bool value) where T : VisualElement, new()
        {
            var conditionalProperty = ReflectionUtility.GetValidMemberInfo(conditionPropertyName, self.Property);
            var memberInfoType = ReflectionUtility.GetMemberInfoType(conditionalProperty);
            if (memberInfoType != null)
            {
                if (memberInfoType == typeof(bool))
                {
                    value = (bool)ReflectionUtility.GetMemberInfoValue(conditionalProperty, self.Property);
                    return true;
                }
            }
            value = false;
            return false;
        }
        
        public static bool TryGetDecoratorsContainerReference<T>(this SerializedDecoratorDrawer<T> self, out VisualElementReference reference) where T : VisualElement, new()
        {
            reference = self.Container.parent.ElementAt(0) as VisualElementReference;
            return reference != null;
        }
        
        public static SerializedDecoratorDrawer<T> Detach<T>(this SerializedDecoratorDrawer<T> self) where T : VisualElement, new()
        {
            VisualElementReference reference;
            if (!self.TryGetDecoratorsContainerReference(out reference))
            {
                var detached = new DetachedDecorators()
                {
                    Origin = self.Container.parent,
                    RelatedField = self.Field,
                    style = { display = DisplayStyle.None }
                };
                self.Field.AddBefore(detached);
                reference = new VisualElementReference() { Reference = detached };
                self.Container.parent.Insert(0, reference);
            }
            if (reference.Reference is not DetachedDecorators container) 
                throw new InvalidCastException("Reference is not a DetachedDecorators VisualElement");
            
            if (self.Container is IElementBlock)
                container.AddBefore(self.Container);
            else
                container.Add(self.Container);

            return self;
        }
    }
}
