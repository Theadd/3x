using System;
using System.Globalization;
using System.Text;
using Unity.Properties;
using UnityEngine;

namespace Space3x.Core.Tests.Samples.Scriptables.LowLevel
{
    public readonly struct PrintContextLow
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
        public PrintContextLow(StringBuilder builder, string prefix, string propertyName)
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
        void PrintValue(in PrintContextLow context, T value);
    }

    // DumpObjectVisitor class that implements various interfaces for property visiting and value printing
    public class DumpObjectVisitorLow : IPropertyBagVisitor, IPropertyVisitor, IPrintValue<Vector2>, IPrintValue<Color>
    {
        private const int k_InitialIndent = 0;

        private readonly StringBuilder m_Builder = new StringBuilder();
        private int m_IndentLevel = k_InitialIndent;

        // Helper property to get the current indentation.
        private string Indent => new (' ', m_IndentLevel * 2);
        
        public void Reset()
        {
            m_Builder.Clear();
            m_IndentLevel = k_InitialIndent;
        }

        public string GetDump()
        {
            return m_Builder.ToString();
        }

        public IPrintValue Adapter { get; set; }

        public DumpObjectVisitorLow()
        {
            // The Adapter property is set to this instance of DumpObjectVisitor
            // This means the current DumpObjectVisitor can be used as a print value adapter for Vector2 and Color.
            Adapter = this;
        }

        // This method is called when visiting a property bag (a collection of properties)
        void IPropertyBagVisitor.Visit<TContainer>(IPropertyBag<TContainer> propertyBag, ref TContainer container)
        {
            foreach (var property in propertyBag.GetProperties(ref container))
            {
                // Call the Visit method of IPropertyVisitor to handle individual properties
                property.Accept(this, ref container);
            }
        }

        // This method is called when visiting each individual property of an object.
        // It tries to find a suitable adapter (IPrintValue<T>) for the property value type (TValue) and uses it to print the value.
        // If no suitable adapter is found, it falls back to displaying the value using its type name.
        void IPropertyVisitor.Visit<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container)
        {
            // Here, we need to manually extract the value.
            var value = property.GetValue(ref container);

            var propertyName = GetPropertyName(property);

            // We can still use adapters, but we must manually dispatch the calls.
            // Try to find an adapter for the current property value type (TValue).
            if (Adapter is IPrintValue<TValue> adapter)
            {
                // If an adapter is found, create a print context and call the PrintValue method of the adapter.
                var context = new PrintContextLow(m_Builder, Indent, propertyName);
                adapter.PrintValue(context, value);
                return;
            }

            // Fallback behavior here - if no adapter is found, handle printing based on type information.
            var type = value?.GetType() ?? property.DeclaredValueType();
            var typeName = TypeUtility.GetTypeDisplayName(type);

            if (TypeTraits.IsContainer(type))
                m_Builder.AppendLine($"{Indent}- {propertyName} {{{typeName}}}");
            else
                m_Builder.AppendLine($"{Indent}- {propertyName} = {{{typeName}}} {value}");

            // Recursively visit child properties (if any).
            ++m_IndentLevel;
            if (null != value)
                PropertyContainer.Accept(this, ref value);
            --m_IndentLevel;
        }

        // Method from IPrintValue<Vector2> used to print Vector2 values
        void IPrintValue<Vector2>.PrintValue(in PrintContextLow context, Vector2 value)
        {
            // Simply use the Print method of PrintContext to print the Vector2 value.
            context.Print(value);
        }

        // Method from IPrintValue<Color> used to print Color values
        void IPrintValue<Color>.PrintValue(in PrintContextLow context, Color value)
        {
            const string format = "F3";
            var formatProvider = CultureInfo.InvariantCulture.NumberFormat;
            
            // Format and print the Color value in RGBA format.
            context.Print(typeof(Color), $"RGBA({value.r.ToString(format, formatProvider)}, {value.g.ToString(format, formatProvider)}, {value.b.ToString(format, formatProvider)}, {value.a.ToString(format, formatProvider)})");
        }
        
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
    }
}
