using System.Text;
using System;
using Unity.Properties;
using UnityEngine;

namespace Space3x.Core.Tests.Samples
{
    public readonly struct PrintContext
    {
        // A context struct to hold information about how to print the property
        private StringBuilder Builder { get; }
        private string Prefix { get; }
        public string PropertyName { get; }

        // Method to print the value of type T with its associated property name
        public void Print<T>(T value)
        {
            Builder.AppendLine($"{Prefix}- {PropertyName} = {{{TypeUtility.GetTypeDisplayName(value?.GetType() ?? typeof(T))}}} {value}");
        }

        // Method to print the value with a specified type and its associated property name
        public void Print(Type type, string value)
        {
            Builder.AppendLine($"{Prefix}- {PropertyName} = {{{TypeUtility.GetTypeDisplayName(type)}}} {value}");
        }

        // Constructor to initialize the PrintContext
        public PrintContext(StringBuilder builder, string prefix, string propertyName)
        {
            Builder = builder;
            Prefix = prefix;
            PropertyName = propertyName;
        }
    }

    // Generic interface IPrintValue that acts as a marker interface for all print value adapters
    public interface IPrintValue
    {
    }

    // Generic interface IPrintValue<T> to define how to print values of type T
    // This interface is used as an adapter for specific types (Vector2 and Color in this case)
    public interface IPrintValue<in T> : IPrintValue
    {
        void PrintValue(in PrintContext context, T value);
    }

    // DumpObjectVisitor class that implements various interfaces for property visiting and value printing
    public class DumpObjectVisitor : PropertyVisitor, IVisitPropertyAdapter<Vector2>, IVisitPropertyAdapter<Color> 
    {
        private const int k_InitialIndent = 0;
        
        // StringBuilder to store the dumped object's properties and values.
        private readonly StringBuilder m_Builder = new StringBuilder();
        private int m_IndentLevel = k_InitialIndent;
        
        // Helper property to get the current indentation.
        private string Indent => new (' ', m_IndentLevel * 2);

        public DumpObjectVisitor()
        {
            // Constructor, it initializes the DumpObjectVisitor and adds itself as an adapter
            // to handle properties of type Vector2 and Color.
            AddAdapter(this);
        }
        
        // Reset the visitor, clearing the StringBuilder and setting indentation to initial level.
        public void Reset()
        {
            m_Builder.Clear();
            m_IndentLevel = k_InitialIndent;
        }

        // Get the string representation of the dumped object.
        public string GetDump()
        {
            return m_Builder.ToString();
        }

        // Helper method to get the property name, handling collections and other property types.
        private static string GetPropertyName(IProperty property)
        {
            return property switch
            {
                // If it's a collection element property, display it with brackets
                ICollectionElementProperty => $"[{property.Name}]",
                // For other property types, display the name as it is
                _ => property.Name
            };
        }

        // This method is called when visiting each property of an object.
        // It determines the type of the value and formats it accordingly for display.
        protected override void VisitProperty<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container, ref TValue value)
        {
            var propertyName = GetPropertyName(property);

            // Get the type of the value or property.
            var type = value?.GetType() ?? property.DeclaredValueType();
            var typeName = TypeUtility.GetTypeDisplayName(type);
            
            // Only display the values for primitives, enums, and strings, and treat other types as containers.
            if (TypeTraits.IsContainer(type))
                m_Builder.AppendLine($"{Indent}- {propertyName} {{{typeName}}}");
            else
                m_Builder.AppendLine($"{Indent}- {propertyName} = {{{typeName}}} {value}");
            
            // Increase indentation level before visiting child properties (if any).
            ++m_IndentLevel;
            if (null != value)
                PropertyContainer.Accept(this, ref value);
            // Decrease indentation level after visiting child properties.
            --m_IndentLevel;
        }

        // This method is a specialized override for Vector2 properties.
        // It displays the property name and its value as a Vector2.
        void IVisitPropertyAdapter<Vector2>.Visit<TContainer>(in VisitContext<TContainer, Vector2> context, ref TContainer container, ref Vector2 value)
        {
            var propertyName = GetPropertyName(context.Property);
            m_Builder.AppendLine($"{Indent}- {propertyName} = {{{nameof(Vector2)}}} {value}");
        }

        // This method is a specialized override for Color properties.
        // It displays the property name and its value as a Color.
        void IVisitPropertyAdapter<Color>.Visit<TContainer>(in VisitContext<TContainer, Color> context, ref TContainer container, ref Color value)
        {
            var propertyName = GetPropertyName(context.Property);
            m_Builder.AppendLine($"{Indent}- {propertyName} = {{{nameof(Color)}}} {value}");
        }
    }
}
