using Core.Config;

namespace Space3x.Core.VirtualEntities
{
    public interface IVirtualEntity { }

    public interface IEntityModule { }

    public interface IEntityModule<T> : IEntityModule, IVirtualEntity { }
    
    public interface IEntityProvider { }
    
    public interface IProps { }
    
    public interface IContextProvider
    {
        public IConfigStore Config { get; }
    }
}
