using System.Linq;
using Space3x.Core.VirtualEntities;
using UnityEditor;
using UnityEngine;

namespace Space3x.Core.Editor
{
    //[CustomEditor(typeof(EntityProvider<IVirtualEntity>), editorForChildClasses: true)]
    [CustomEditor(typeof(IEntityProvider), editorForChildClasses: true)]
    public class EntityProviderEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // GUI.enabled = Application.isPlaying;
            var baseType = target.GetType();
            var genericArgs = baseType.GetGenericArguments().Select(t => t.Name).ToArray();
            var nestedTypes = baseType.GetNestedTypes().Select(t => t.Name).ToArray();
            var interfaces = baseType.GetInterfaces().Select(t => t.Name).ToArray();

            DrawItemsOnGUI(genericArgs, "Generic Arguments");
            DrawItemsOnGUI(nestedTypes, "Nested Types");
            DrawItemsOnGUI(interfaces, "Interfaces");

        }

        private void DrawItemsOnGUI(string[] items, string headerText)
        {
            GUILayout.Label(headerText + $" ({items.Length})");
            foreach (var item in items)
            {
                GUILayout.TextField(item);
            }
            GUILayout.Space(12f);
        }
    }
}
