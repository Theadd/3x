using UnityEngine;

namespace Space3x.Core.Attributes
{
    public class VisibleAttribute : PropertyAttribute
    {
        public bool isVisible = true;
        public string condition = string.Empty;
        
        public VisibleAttribute() { }
        
        public VisibleAttribute(bool isVisible) => this.isVisible = isVisible;
        
        public VisibleAttribute(string condition) => this.condition = condition;
        
        public VisibleAttribute(string condition, bool isVisible)
        {
            this.condition = condition;
            this.isVisible = isVisible;
        }
    }
}
