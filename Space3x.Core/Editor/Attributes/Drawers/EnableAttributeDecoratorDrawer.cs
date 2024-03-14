using UnityEditor;
using Space3x.Core.Attributes;
using Space3x.Core.Editor.Attributes.VisualElements;
using Space3x.Core.Editor.Extensions;

namespace Space3x.Core.Editor.Attributes.Drawers
{
    [CustomPropertyDrawer(typeof(EnableAttribute))]
    public class EnableAttributeDecoratorDrawer : SerializedDecoratorDrawer<AutoDecorator>
    {
        private EnableAttribute Target => (EnableAttribute) attribute;

        protected override bool RedrawOnAnyValueChange => Target.condition != string.Empty;

        protected override void OnPropertyDraw()
        {
            if (Property != null) {
                if (Target.condition != string.Empty) {
                    if (this.TryGetConditionValue(Target.condition, out var isTrue)) {
                        Field.SetEnabled(isTrue ? Target.isEnabled : !Target.isEnabled);
                        return;
                    }
                }
            }
            
            Field.SetEnabled(Target.isEnabled);
        }
    }
}
