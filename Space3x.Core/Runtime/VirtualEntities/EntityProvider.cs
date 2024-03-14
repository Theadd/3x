#if UNITY_EDITOR
using Space3x.Core.Extensions;
#endif

namespace Space3x.Core.VirtualEntities
{
    public abstract class EntityProvider<T> : ContextProvider, IEntityProvider where T : class
    {
        public VirtualEntity<T> Instance { get; } = new VirtualEntity<T>();
        
#if UNITY_EDITOR
        protected virtual void OnEnable() => this.AddEditorIcon();
        protected virtual void OnDisable() => this.RemoveEditorIcon();
#endif
    }
}
