using System;
using UnityEngine;

namespace Space3x.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class NoScriptAttribute : PropertyAttribute
    {
        public NoScriptAttribute() { }
    }
}
