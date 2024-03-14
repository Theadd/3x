using Space3x.Core.Attributes;
using UnityEngine;
using UnityEngine.UIElements;

namespace Space3x.Core.Editor.Attributes.VisualElements
{
    [UxmlElement]
    public partial class PropertyGroup : VisualElement
    {
        [UxmlAttribute]
        public string GroupName { get; set; } = string.Empty;
        
        public GroupType Type { get; set; }
        
        public PropertyGroup() => AddToClassList($"ui3x-property-group");
        
        public virtual bool GroupContains(VisualElement element) => element.parent == contentContainer || element.parent == this;
    }
}
