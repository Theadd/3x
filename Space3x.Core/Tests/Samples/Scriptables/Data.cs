using System.Collections.Generic;
using UnityEngine;

namespace Space3x.Core.Tests.Samples.Scriptables
{
    public class Data
    {
        public string Name = "Henry";
        public Vector2 Vec2 = Vector2.one;
        public List<Color> Colors = new List<Color> { Color.green, Color.red };
        public Dictionary<int, string> Dict = new Dictionary<int, string> {{5, "zero"}};
    }
}
