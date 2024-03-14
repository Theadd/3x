using Space3x.Core.Attributes;
using Space3x.Core.Tests.Samples;
using Space3x.Core.Tests.Samples.Scriptables;
using Space3x.Core.Tests.Samples.Scriptables.LowLevel;
using UnityEngine;

namespace Space3x.Core.Tests
{
    public class AnotherSampleComponent : MonoBehaviour
    {
//        [NoScript]
//        [Range(5, 15)]
        public int number;
        public string text = "Hello World";
        public bool showAdvanced = true;
        [Layer]
        public int targetLayer;
//        public string hardTextNormal = "Hello World 0";
//        [Enable(nameof(HideAdvanced))]
//        public string hardTextVisible = "Hello World 1";
//        [Enable(nameof(showAdvanced))]
//        public string hardTextNotVisible = "Hello World 2";
//        [Multiline(5)]
//        public string hardTextVisibleDefault = "Hello World 3";
//        [Visible(nameof(showAdvanced))]
//        public string hardTextVisibleCondition = "Hello World CONDITION";
//        public List<int> list = new List<int>();
//
//        public bool HideAdvanced() => !showAdvanced;

        [Button(nameof(Dump))]
        public string buttonTest = "Click Me!";


        public void Dump()
        {
            DebugUtilities.PrintObjectDump(new Data());
            Debug.LogWarning("LOW:");
            DebugUtilitiesLow.PrintObjectDump(new Data());
        }
    }
}
