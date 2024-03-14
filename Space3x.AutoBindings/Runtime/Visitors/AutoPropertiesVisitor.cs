using System.Collections.Generic;
using Space3x.AutoBindings.Runtime.Attributes;
using Unity.Properties;

namespace Space3x.AutoBindings.Runtime
{
    public class AutoPropertiesVisitor : PropertyVisitor {
        public List<PropertyPath> AutoProperties { get; set; }

        protected override void VisitProperty<TContainer, TValue>(Property<TContainer, TValue> property,
            ref TContainer container, ref TValue value)
        {
            if (property.HasAttribute<AutoAttribute>())
                AutoProperties.Add(PropertyPath.AppendProperty(default, property));
        }
    }
}
