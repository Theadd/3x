using Space3x.Core.Editor.Attributes.VisualElements;
using Space3x.Core.Editor.Extensions;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Space3x.Core.Editor.Attributes.Drawers
{
    public abstract class SerializedDecoratorDrawer<T> : DecoratorDrawer where T : VisualElement, new()
    {
        public PropertyField Field { get; set; }
        public SerializedProperty Property { get; set; }
        
        public T Container { get; private set; }
        
        private bool _detached;
        
        protected virtual bool RedrawOnAnyValueChange => false;
        
        protected virtual void OnCreatePropertyGUI(VisualElement container) {}

        protected virtual void OnPropertyDraw() {}
        
        public override VisualElement CreatePropertyGUI()
        {
            Container = new T() { name = attribute.GetType().Name + "_Drawer" };
            OnCreatePropertyGUI(Container);
            Container.RegisterCallbackOnce<AttachToPanelEvent>(OnAttachToPanel);

            return Container;
        }

        private void OnAttachToPanel(AttachToPanelEvent ev)
        {
            if (_detached)
            {
                if (((VisualElement) ev.target).parent is DetachedDecorators detachedDecorators)
                    OnAttachToDetachedDecorators(detachedDecorators);
                return;
            }
            BindToClosestParentPropertyFieldOf((VisualElement)ev.target);
            Container.UnregisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            if (Field != null)
            {
                _detached = true;
                this.Detach();
            }
            OnPropertyDraw();
        }

        /// <summary>
        /// Only called in derived classes where T (from SerializedDecoratorDrawer<T>) doesn't implement
        /// the IElementBlock interface. Classes such as ButtonDecorator do implement IElementBlock hence
        /// the OnAttachToDetachedDecorators method is not called for them. That's because they don't have
        /// a parent of type DetachedDecorators.
        /// </summary>
        /// <param name="parent"></param>
        protected virtual void OnAttachToDetachedDecorators(DetachedDecorators parent) { }

        private void BindToClosestParentPropertyFieldOf(VisualElement target)
        {
            Field = target.GetClosestParentOfType<PropertyField, InspectorElement>();
            if (Field == null)
            {
                Debug.LogWarning($"Could not find parent PropertyField of {target.name} for {attribute.GetType().Name}\n");
                return;
            }
            Property = typeof(PropertyField).GetField(
                    "m_SerializedProperty", 
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.GetValue(Field) as SerializedProperty;
            if (RedrawOnAnyValueChange)
                TrackAllChangesOnInspectorElement();
        }

        private void TrackAllChangesOnInspectorElement()
        {
            var inspectorElement = Field.GetClosestParentOfType<InspectorElement>();
            inspectorElement?.TrackSerializedObjectValue(Property.serializedObject, OnSerializedObjectValueChanged);
        }
        
        private void OnSerializedObjectValueChanged(SerializedObject serializedObject)
        {
            OnPropertyDraw();
        }
    }
}
