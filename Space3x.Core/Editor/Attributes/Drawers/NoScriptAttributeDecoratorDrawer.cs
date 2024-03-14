using UnityEditor;
using Space3x.Core.Attributes;
using Space3x.Core.Editor.Attributes.VisualElements;
using Space3x.Core.Editor.Extensions;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Space3x.Core.Editor.Attributes.Drawers
{
    [CustomPropertyDrawer(typeof(NoScriptAttribute))]
    public class NoScriptAttributeDecoratorDrawer : SerializedDecoratorDrawer<AutoDecorator>
    {
        protected override void OnPropertyDraw()
        {
            if (Field != null)
            {
                var root = Field.GetClosestParentOfType<InspectorElement>();
                root.Q<PropertyField>("PropertyField:m_Script").style.display = DisplayStyle.None;
            }
        }
    }
}
