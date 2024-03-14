using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Space3x.Core.Attributes;

namespace Space3x.Core.Editor.Attributes.Drawers
{
    [CustomPropertyDrawer(typeof(LayerAttribute))]
    public class LayerAttributePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var field = new LayerField() { label = property.displayName };
            field.AddToClassList(BaseField<int>.alignedFieldUssClassName);
            field.BindProperty(property);
            return field;
        }
    }
}
