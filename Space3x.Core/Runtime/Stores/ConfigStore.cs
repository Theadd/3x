using System;
using UnityEngine;

// WARNING: THIS IS AN EARLY DRAFT!
namespace Core.Config
{
    public interface IConfigChanged { }
    
    public interface IConfigStore { }
        
    public interface IConfigStore<T> : IConfigStore { }
    
    [Serializable]
    public partial class ConfigStore : IConfigStore
    {

    }
    
    public partial class ConfigStore : IConfigStore
    {

    }
    
    public partial class ConfigStore<T> : ConfigStore, IConfigStore<T>
    {
        public event Action<IConfigChanged> OnChangeConfig;

        public void SetConfig(IConfigStore config)
        {
            OnChangeConfig += Dummy;
            OnChangeConfig.Invoke(null);
        }
        
        private void Dummy(IConfigChanged _) { }
        
        // COPY PASTE
        
        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        public void LoadJson(string jsonString)
        {
            JsonUtility.FromJsonOverwrite(jsonString, this);
        }
        
        
    }
}
