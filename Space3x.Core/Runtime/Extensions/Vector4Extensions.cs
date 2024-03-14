﻿// @source: https://github.com/DanielEverland/ScriptableObject-Architecture
using UnityEngine;

namespace Space3x.Core.Extensions
{
    /// <summary>
    /// Internal extension methods for <see cref="Vector4"/>.
    /// </summary>
    public static class Vector4Extensions
    {
        /// <summary>
        /// Returns a <see cref="Quaternion"/> instance where the component values are equal to this
        /// <see cref="Vector4"/>'s components.
        /// </summary>
        /// <param name="vector4"></param>
        /// <returns></returns>
        public static Quaternion ToQuaternion(this Vector4 vector4)
        {
            return new Quaternion(vector4.x, vector4.y, vector4.z, vector4.w);
        }
    }
}