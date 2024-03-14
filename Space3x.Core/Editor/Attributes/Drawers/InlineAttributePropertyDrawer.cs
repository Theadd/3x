using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Space3x.Core.Attributes;

namespace Space3x.Core.Editor.Attributes.Drawers
{
    [CustomPropertyDrawer(typeof(InlineAttribute))]
    public class InlineAttributePropertyDrawer : PropertyDrawer
    {
        public VisualElement InspectorContainer { get; private set; }
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();
            var field = new PropertyField(property);
            field.Unbind();
            field.TrackPropertyValue(property, CheckInline);
            field.BindProperty(property);
            InspectorContainer = new VisualElement();
            container.Add(field);
            container.Add(InspectorContainer);
            CheckInline(property);
            
            return container;
        }
        
        private void CheckInline(SerializedProperty property)
        {
            InspectorContainer.Clear();
            if (property.objectReferenceValue != null)
            {
                var inlineInspector = new InspectorElement(property.objectReferenceValue);
                InspectorContainer.Add(inlineInspector);
            }
        }
    }
}
