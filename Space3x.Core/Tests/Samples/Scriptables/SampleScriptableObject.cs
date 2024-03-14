using System;
using System.Collections.Generic;
using UnityEngine;

namespace Space3x.Core.Samples.Scriptables
{
    [CreateAssetMenu(fileName = "MySampleScriptableObject", menuName = "Space3x/Samples/Create Sample Scriptable Object", order = 0)]
    public class SampleScriptableObject : ScriptableObject
    {
        public string Text;
        public int Number;
        public bool ShowAdvanced;
        public int TargetLayer;
        public string Description;
        public List<Type> Types = new List<Type>();
        
        
        public bool HideAdvanced() => !ShowAdvanced;
        public void RunAction01() => Debug.Log("Action 01");
        public void RunAction02() => Debug.Log("Action 02");
        public void RunAction03() => Debug.Log("Action 03");
        
        
    }
}
