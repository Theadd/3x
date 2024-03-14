using Core.VirtualEntities;
using UnityEngine;
using Space3x.Core.Runtime;

namespace Core.Config
{
    public partial interface IPlayerConfig : IConfigStore<IPlayer>
    {
        public bool InvertPitch { get; }
        public WorldPosition SpawnPoint { get; }
        
    }
    
    public partial class PlayerConfig : ConfigStore<IPlayer>, IPlayerConfig
    {
        private LocalConfig _config = new LocalConfig();

        public bool InvertPitch => _config.InvertPitch;

        public WorldPosition SpawnPoint => _config.SpawnPoint;
        
        private partial class LocalConfig : IPlayerConfig
        {
            public bool InvertPitch { get; protected set; } = false;

            public WorldPosition SpawnPoint { get; protected set; } = new WorldPosition() { Position = new Vector4(70f, -5f, -70f, 0f) };
            
            public LocalConfig() { }
        }
    }
}
