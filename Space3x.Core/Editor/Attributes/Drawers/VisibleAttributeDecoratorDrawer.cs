using UnityEditor;
using UnityEngine.UIElements;
using Space3x.Core.Attributes;
using Space3x.Core.Editor.Attributes.VisualElements;
using Space3x.Core.Editor.Extensions;
using UnityEngine;

namespace Space3x.Core.Editor.Attributes.Drawers
{
    [CustomPropertyDrawer(typeof(VisibleAttribute))]
    public class VisibleAttributeDecoratorDrawer : SerializedDecoratorDrawer<AutoDecorator>
    {
        private VisibleAttribute Target => (VisibleAttribute) attribute;

        protected override bool RedrawOnAnyValueChange => Target.condition != string.Empty;

        protected override void OnPropertyDraw()
        {
            if (Property != null) {
                if (Target.condition != string.Empty) {
                    if (this.TryGetConditionValue(Target.condition, out var isTrue)) {
                        SetVisible(Field, isTrue ? Target.isVisible : !Target.isVisible);
                        return;
                    }
                }
            }
            
            SetVisible(Field, Target.isVisible);
        }
        
        private void SetVisible(VisualElement propertyField, bool visible)
        {
            propertyField.style.display = visible ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}
