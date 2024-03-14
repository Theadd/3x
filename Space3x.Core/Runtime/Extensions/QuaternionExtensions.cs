// @source: https://github.com/DanielEverland/ScriptableObject-Architecture
using UnityEngine;

namespace Space3x.Core.Extensions
{
    /// <summary>
    /// Internal extension methods for <see cref="Quaternion"/>.
    /// </summary>
    public static class QuaternionExtensions
    {
        /// <summary>
        /// Returns a <see cref="Vector4"/> instance where the component values are equal to this
        /// <see cref="Quaternion"/>'s components.
        /// </summary>
        /// <param name="quaternion"></param>
        /// <returns></returns>
        public static Vector4 ToVector4(this Quaternion quaternion)
        {
            return new Vector4(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
        }
        
        public static Quaternion ToLocal(this Quaternion worldRotation, Quaternion parentWorldRotation) => Quaternion.Inverse(parentWorldRotation) * worldRotation;
        
        public static Quaternion ToWorld(this Quaternion localRotation, Quaternion parentWorldRotation) => parentWorldRotation * localRotation;
    }
}
