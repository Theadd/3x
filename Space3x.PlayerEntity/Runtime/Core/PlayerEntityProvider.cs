using Core.Config;
using Space3x.Core.VirtualEntities;
using UnityEngine;

namespace Core.VirtualEntities
{
    /// <summary>
    /// Interface to identify all classes handled by the same VirtualEntity type instance.
    /// </summary>
    public interface IPlayer : IEntityModule<IPlayer>, IVirtualEntity { }

    /// <summary>
    /// Each GameObject in scene having an EntityProvider component is grouping all other
    /// nested components implementing the same IVirtualEntity Type (in this case,
    /// the IPlayer) down in hierarchy. If there's another EntityProvider component
    /// of the same IVirtualEntity Type within the nested children, a new group will be
    /// created, delimiting and excluding their descendents from the previous group.
    /// </summary>
    [ExecuteInEditMode]
    public partial class PlayerEntityProvider : EntityProvider<IPlayer>, IPlayer
    {
        public new PlayerConfig Config => base.Config as PlayerConfig;
        protected override IConfigStore LoadConfig()
        {
            return new PlayerConfig();
        }
    }
}
