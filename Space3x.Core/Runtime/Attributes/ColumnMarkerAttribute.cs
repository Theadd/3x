using System;

namespace Space3x.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class
                    | AttributeTargets.Method
                    | AttributeTargets.Property
                    | AttributeTargets.Field,
        AllowMultiple = true, Inherited = true)]
    public class BeginColumnAttribute : GroupMarkerAttribute
    {
        public BeginColumnAttribute() : base(GroupType.Column) { IsOpen = true; }
    }

    [AttributeUsage(AttributeTargets.Class
                    | AttributeTargets.Method
                    | AttributeTargets.Property
                    | AttributeTargets.Field,
        AllowMultiple = true, Inherited = true)]
    public class EndColumnAttribute : GroupMarkerAttribute
    {
        public EndColumnAttribute() : base(GroupType.Column) { IsOpen = false; }
    }
}
