using Space3x.Core.Attributes;
using UnityEngine;
using UnityEngine.UIElements;

namespace Space3x.Core.Editor.Attributes.VisualElements
{
    [UxmlElement]
    public partial class GroupMarkerDecorator : VisualElement
    {
        public GroupType Type { get; set; }
        
        public string GroupName { get; set; }
        
        public VisualElement Origin { get; set; }
        
        public bool IsOpen { get; set; }
        
        public bool IsUsed { get; private set; } = false;

        public void Use() => IsUsed = true;
        
        public GroupMarkerDecorator() => AddToClassList($"ui3x-group-marker");
    }
}
