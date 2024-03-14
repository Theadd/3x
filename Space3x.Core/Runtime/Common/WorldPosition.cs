using UnityEngine;

namespace Space3x.Core.Runtime
{
    public class WorldPosition : MonoBehaviour
    {
        public Vector4 Position { get; set; }
        
        public static implicit operator Vector3(WorldPosition other) => other.Position;
        public static implicit operator Vector4(WorldPosition other) => other.Position;
        public static implicit operator Vector2(WorldPosition other) => other.Position;
    }
}
