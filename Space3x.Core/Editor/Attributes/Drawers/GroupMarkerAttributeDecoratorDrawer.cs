using UnityEditor;
using Space3x.Core.Attributes;
using Space3x.Core.Editor.Attributes.VisualElements;
using Space3x.Core.Editor.Extensions;

namespace Space3x.Core.Editor.Attributes.Drawers
{
    [CustomPropertyDrawer(typeof(GroupMarkerAttribute), true)]
    public class GroupMarkerAttributeDecoratorDrawer : SerializedDecoratorDrawer<AutoDecorator>
    {
        public GroupMarkerDecorator Marker { get; set; }
        
        public GroupMarkerAttribute Target => (GroupMarkerAttribute) attribute;

        protected override void OnAttachToDetachedDecorators(DetachedDecorators parent)
        {
            if (Marker != null) return;
            Marker = this.CreateMarker();
            parent.AddBefore(Marker);
            if (!Marker.IsOpen)
                Marker.CloseGroupMarker();
        }
    }
}
