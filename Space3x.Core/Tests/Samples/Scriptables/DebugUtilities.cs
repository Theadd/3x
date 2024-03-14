using Unity.Properties;
using UnityEngine;

namespace Space3x.Core.Tests.Samples
{
    public static class DebugUtilities
    {
        private static readonly DumpObjectVisitor s_Visitor = new();

        public static void PrintObjectDump<T>(T value, PropertyPath path = default)
        {
            s_Visitor.Reset();
            if (path.IsEmpty)
                PropertyContainer.Accept(s_Visitor, ref value);
            else
                PropertyContainer.Accept(s_Visitor, ref value, path);
            Debug.Log(s_Visitor.GetDump());
        }
    }
}
