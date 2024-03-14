#if UNITY_EDITOR
using System.Collections.Generic;
using Space3x.Core.VirtualEntities;
using UnityEditor;
using UnityEngine;

namespace Space3x.Core.Extensions
{
    public static class EditorExtensions
    {
        public static bool ShowNumericIconsInHierarchy { get; set; } = true;

        private static HashSet<int> _instanceIds = new HashSet<int>();
        private static List<Dictionary<int, int>> _values = new List<Dictionary<int, int>>()
        {
            new Dictionary<int, int>()
        };

        private static string[] _labels = new[] { "ENT" };
        
        private static GUIStyle IconStyle { get; } = new GUIStyle()
        {
            fontSize = 7,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleLeft,
            contentOffset = new Vector2(-26, 0),
            // below lines are redundant since those values are the default ones
            padding = new RectOffset(0, 0, 0, 0),
            margin = new RectOffset(0, 0, 0, 0),
            border = new RectOffset(0, 0, 0, 0),
        };

        private static GUIStyle IconStyle01 { get; } = new GUIStyle(IconStyle)
        {
            normal = new GUIStyleState() { textColor = (Color) new Color32(0, 221, 255, 159)}
        };
        
        private static GUIStyle[] Styles { get; } = new[]
        {
            IconStyle01
        };

        private static void SetEditorIconValue(int listIndex, int instanceId, int value) => 
            _values[listIndex][instanceId] = value;

        private static void RemoveEditorIconValue(int listIndex, int instanceId) =>
            _values[listIndex].Remove(instanceId);

        private static void DrawEditorIconLabel(int instanceId, Rect rect)
        {
            for (var i = 0; i < _values.Count; i++)
            {
                if (_values[i].ContainsKey(instanceId))
                {
                    var value = _values[i][instanceId];
                    var label = (value < 0) ? _labels[Mathf.Abs(value) - 1] : value.ToString();
                    GUI.Label(rect, new GUIContent(label, label), Styles[i]);
                    return;
                }
            }
        }

        public static void AddEditorIcon<T>(this EntityProvider<T> self) where T : class
        {
            var instanceId = self.gameObject.GetInstanceID();
            if (_instanceIds.Count == 0) RegisterCallbackOnHierarchyGUI();
            _instanceIds.Add(instanceId);
            SetEditorIconValue(0, instanceId, ShowNumericIconsInHierarchy ? self.Instance.Count : -1);
        }
        
        public static void RemoveEditorIcon<T>(this EntityProvider<T> self) where T : class
        {
            _instanceIds.Remove(self.gameObject.GetInstanceID());
            if (_instanceIds.Count == 0) UnRegisterCallbackOnHierarchyGUI();
            RemoveEditorIconValue(0, self.GetInstanceID());
        }

        private static void RegisterCallbackOnHierarchyGUI()
        {
            EditorApplication.hierarchyWindowItemOnGUI -= HierarchyWindowItemOnGUI;
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
        }
        
        private static void UnRegisterCallbackOnHierarchyGUI()
        {
            EditorApplication.hierarchyWindowItemOnGUI -= HierarchyWindowItemOnGUI;
        }

        public static void HierarchyWindowItemOnGUI(int instanceId, Rect rect)
        {
            if (_instanceIds.Contains(instanceId))
            {
                // Debug.Log($"RECT = {rect.ToString()}");
                // var r = new Rect(rect) { x = rect.x - 26, width = 24 };
                DrawEditorIconLabel(instanceId, rect);
                // GUI.Label(r, new GUIContent("ENT", "VirtualEntity Provider"), IconStyle);
            }
        }
    }
}
#endif
