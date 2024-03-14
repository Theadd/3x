namespace Space3x.AutoBindings.Runtime
{
    public interface IBindValue { }

    public interface IBindValue<in T> : IBindValue
    {
        void BindValue(in AutoBindingContext context, T value);
    }
    
    /// <summary>
    /// A context struct to hold information about how to deal with the property
    /// </summary>
    public readonly struct AutoBindingContext
    {
        public string PropertyName { get; }
        
        public AutoBindingContext(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}
