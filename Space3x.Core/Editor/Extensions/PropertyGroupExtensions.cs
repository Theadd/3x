using System.Linq;
using Space3x.Core.Editor.Attributes.Drawers;
using Space3x.Core.Editor.Attributes.VisualElements;
using Space3x.Core.Editor.Utilities;
using UnityEngine;
using UnityEngine.UIElements;

namespace Space3x.Core.Editor.Extensions
{
    public static class PropertyGroupExtensions
    {
        public static void AddToGroup(this PropertyGroup group, VisualElement element)
        {
            group.Add(element);
            VisualStyle.ApplyStyles(group.Type, element);
        }

        public static GroupMarkerDecorator CreateMarker(this GroupMarkerAttributeDecoratorDrawer self)
        {
            return new GroupMarkerDecorator()
            {
                Type = self.Target.Type,
                GroupName = self.Target.Text,
                Origin = self.Container,
                IsOpen = self.Target.IsOpen,
                style = { display = DisplayStyle.None }
            };
        }
        
        public static void CloseGroupMarker(this GroupMarkerDecorator endMarker)
        {
            var parent = endMarker.parent;
            var beginMarker = endMarker.GetMatchingGroupMarker();
            if (beginMarker == null)
            {
                Debug.LogError("Couldn't find matching group marker");
                return;
            }
            var endIndex = parent.IndexOf(endMarker);
            var beginIndex = parent.IndexOf(beginMarker);

            var rawNodes = parent.Children()
                .Skip(beginIndex)
                .Take(endIndex - beginIndex + 1).ToList();
            
            var group = new PropertyGroupField()
            {
                Text = beginMarker.GroupName,
                GroupName = beginMarker.GroupName,
                Type = beginMarker.Type,
                style =
                {
                    flexDirection = FlexDirection.Row
                },
                contentContainer =
                {
                    style =
                    {
                        flexDirection = FlexDirection.Row,
                        flexGrow = 1,
                        flexShrink = 1
                    }
                }
            };
            group.contentContainer.RowGrowShrink();
            parent.Insert(beginIndex, group);
            rawNodes.ForEach(group.AddToGroup);
            
            beginMarker.Use();
            endMarker.Use();
        }

        public static GroupMarkerDecorator GetMatchingGroupMarker(this GroupMarkerDecorator endMarker)
        {
            var parent = endMarker.parent;
            var endIndex = parent.IndexOf(endMarker);
            var allNodes = parent.Children()
                .Take(endIndex + 1);

            var beginMarker = allNodes.Last(
                node => node is GroupMarkerDecorator marker
                        && (marker.Type == endMarker.Type && marker.IsOpen && !marker.IsUsed));
            
            return beginMarker as GroupMarkerDecorator;
        }
    }
}
