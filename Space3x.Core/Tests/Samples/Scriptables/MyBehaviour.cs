using UnityEngine;
using Unity.Properties;

namespace Space3x.Core.Samples.Scriptables
{
    public class MyBehaviour : MonoBehaviour
    {
        // Serializations go through the field, but we don't want to create a property for it.
        [SerializeField, DontCreateProperty] 
        private int m_Value;
    
        // For the property bag, use the property instead of the field. This ensures that
        // the value stays within the appropriate bounds.
        [CreateProperty] 
        public int value
        {
            get => m_Value;
            set => m_Value = value;
        }
    
        // This is a similar example, but for an auto-property.
        [field: SerializeField, DontCreateProperty]
        [CreateProperty]
        public float floatValue { get; set; }
    }
}
