using UnityEngine;
using UnityEngine.UIElements;

namespace Space3x.Core.Editor.Attributes.VisualElements
{
    [UxmlElement]
    public partial class PropertyGroupField : PropertyGroup
    {
        [UxmlAttribute]
        public string Text
        {
            get => _label.text;
            set
            {
                _label.text = value;
                Update();
            }
        }

        private VisualElement _container;
        private Label _label;

        private static readonly string[] LabelClassNames = {
            "unity-text-element", 
            "unity-label", 
            "unity-base-field__label", 
            "unity-property-field__label"
        };

        public PropertyGroupField()
        {
            _label = new Label(string.Empty);
            foreach (var className in LabelClassNames)
            {
                _label.EnableInClassList(className, true);
            }
            Add(_label);
            var container = new PropertyGroup();
            Add(container);
            _container = container;
            AddToClassList($"ui3x-property-group-field");
            AddToClassList($"ui3x-group-type__{Type.ToString().ToLower()}");
            RegisterCallbackOnce<AttachToPanelEvent>(OnAttachToPanel);
        }

        private void OnAttachToPanel(AttachToPanelEvent ev) => Update();

        private void Update()
        {
            _label.style.display = _label.text == string.Empty ? DisplayStyle.None : DisplayStyle.Flex;
        }
        
        public override VisualElement contentContainer => _container ?? this;
        
        public override bool GroupContains(VisualElement element) => element.parent == contentContainer || element.parent == this;
    }
}
