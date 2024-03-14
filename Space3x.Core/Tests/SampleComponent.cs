using System.Collections.Generic;
using Space3x.Core.Attributes;
using Space3x.Core.Tests.Samples;
using Space3x.Core.Tests.Samples.Scriptables;
using Unity.Properties;
using UnityEngine;

namespace Space3x.Core.Tests
{
    public class SampleComponent : MonoBehaviour
    {
        [NoScript]
        [Range(5, 15)]
        public int number;
        [Header("# HEADER")]
        public string text = "Hello World";
        
        [BeginRow]
        [CreateProperty]
        public bool showAdvanced = true;
        [Layer]
        public int targetLayer;
        [BeginRow]
        
        public int r1c1;
        [Enable(nameof(showAdvanced))]
        public int r1c2;
        public int r1c3;
        
        [BeginRow]
        public string hardTextNormal = "Hello World 0";
        
        [Enable(nameof(HideAdvanced))]
        public string hardTextVisible = "Hello World 1";
        [EndRow]
        [Enable(nameof(showAdvanced))]
        public string hardTextNotVisible = "Hello World 2";
        [Multiline(5)]
        public string hardTextVisibleDefault = "Hello World 3";
        [Visible(nameof(showAdvanced))]
        public string hardTextVisibleCondition = "Hello World CONDITION";
        
        [BeginRow(Text = "<b>Actions</b>")]
        public List<int> list = new List<int>();

        [BeginColumn]
        [Button(nameof(ClickMe))]
        [Button(nameof(AnotherClick))]
        [Button(nameof(YetAnotherClick))]
        [EndColumn]
        
        [EndRow]
        public string buttonTest = "Click Me!";
        
        [Inline]
        public AnotherSampleComponent anotherSampleComponent;
        public bool HideAdvanced() => !showAdvanced;
        
        public void ClickMe()
        {
            Debug.Log("Clicked");
            number++;
            showAdvanced = !showAdvanced;
        }

        public void AnotherClick()
        {
            DebugUtilities.PrintObjectDump(new Data());
        }

        public void YetAnotherClick() => ClickMe();

    }
}
