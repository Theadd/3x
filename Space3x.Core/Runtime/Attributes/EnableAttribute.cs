using UnityEngine;

namespace Space3x.Core.Attributes
{
    public class EnableAttribute : PropertyAttribute
    {
        public bool isEnabled = true;
        public string condition = string.Empty;
        
        public EnableAttribute() { }
        
        public EnableAttribute(bool isEnabled) => this.isEnabled = isEnabled;
        
        public EnableAttribute(string condition) => this.condition = condition;
        
        public EnableAttribute(string condition, bool isEnabled)
        {
            this.condition = condition;
            this.isEnabled = isEnabled;
        }
    }
}
