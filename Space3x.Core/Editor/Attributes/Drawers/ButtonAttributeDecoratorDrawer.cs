using System.Reflection;
using UnityEditor;
using Space3x.Core.Attributes;
using Space3x.Core.Editor.Attributes.VisualElements;
using Space3x.Core.Editor.Utilities;
using UnityEngine.UIElements;

namespace Space3x.Core.Editor.Attributes.Drawers
{
    [CustomPropertyDrawer(typeof(ButtonAttribute))]
    public class ButtonAttributeDecoratorDrawer : SerializedDecoratorDrawer<ButtonDecorator>
    {
        private ButtonAttribute Target => (ButtonAttribute) attribute;

        private Button _button;
        private MethodInfo _buttonMethod;
        
        protected override void OnCreatePropertyGUI(VisualElement container)
        {
            _button = new Button(OnClick)
            {
                text = Target.methodName,
                name = "ui-button_" + Target.methodName
            };
            container.Add(_button);
        }
        
        private void OnClick()
        {
            if (_buttonMethod != null)
                _buttonMethod.Invoke(Property.serializedObject.targetObject, null);
        }
        
        protected override void OnPropertyDraw()
        {
            if (Property == null) return;
            var target = Property.serializedObject.targetObject;
            _buttonMethod = ReflectionUtility.FindFunction(Target.methodName, target);
        }
    }
}
