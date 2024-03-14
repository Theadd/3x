using System;
using UnityEngine;

namespace Space3x.Core.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class ButtonAttribute : PropertyAttribute
    {
        public string methodName = string.Empty;
        
        public ButtonAttribute() { }
        
        public ButtonAttribute(string methodName) => this.methodName = methodName;
    }
}
