using Core.Config;
using UnityEngine;

namespace Space3x.Core.VirtualEntities
{
    public abstract class ContextProvider : MonoBehaviour, IContextProvider
    {
        private IConfigStore _config;
        
        /// <summary>
        /// Gets the config stored by this provider.
        /// </summary>
        public IConfigStore Config
        {
            get => _config ?? LoadConfig();
            protected set => _config = value;
        }

        protected abstract IConfigStore LoadConfig();
    }
}
