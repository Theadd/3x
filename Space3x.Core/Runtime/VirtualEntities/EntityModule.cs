using System.Collections.Generic;
using Core.Config;
using Space3x.Core.Extensions;
using UnityEngine;

namespace Space3x.Core.VirtualEntities
{
    public abstract class EntityModule<T> : MonoBehaviour, IEntityModule<T> where T : class
    {
        private VirtualEntity<T> _entity;
        private IContextProvider _provider;

        // TODO
        public virtual void OnConfigChanged() { }

        /// <summary>
        /// Gets direct access to the local settings within the scope of this component's
        /// EntityProvider, which should be only those values accessible via IConfigStore<**T**>,
        /// where **T** is the same one used in this class signature as the generic argument
        /// provided by EntityModule<**T**>. To access config values other than those, <see cref=""/>  
        /// </summary>
        public IConfigStore<T> Config => (_provider ??= transform.LocalProvider<T>()).Config as IConfigStore<T>;

        public IConfigStore<T2> GetConfig<T2>() where T2 : class
        {
            Debug.LogWarning("Although it works as expected, it's just to get by as currently " +
                             "it just walks up the hierarchy looking for a matching provider to get " +
                             "it's local config, for every call. TODO: Some pub/sub-like system on" +
                             "the ContextProvider, which should be the one responsible of deriving " +
                             "to other context providers or not.", this);
            return transform.LocalProvider<T2>()?.Config as IConfigStore<T2>;
        }

        public virtual T2 Get<T2>() where T2 : class => EntityController().Get<T2>() as T2;
        
        public virtual TResult Get<TFind, TResult>() where TResult : class => EntityController().Get<TFind>() as TResult;
        
        public virtual IEnumerable<T2> OfType<T2>() where T2 : class => EntityController().OfType<T2>();
        
        public virtual IEnumerable<TResult> OfType<TFind, TResult>() where TResult : class => EntityController().OfType<TFind, TResult>();

        public virtual EntityProvider<T> GetContext() => (_provider ??= transform.LocalProvider<T>()) as EntityProvider<T>;
        
        public virtual TProvider GetContext<TProvider>() where TProvider : class => (_provider ??= transform.LocalProvider<T>()) as TProvider;
        
        public virtual TProps GetProps<TProps>() where TProps : class => GetContext() as TProps;

        public virtual VirtualEntity<T> EntityController() => 
            _entity ??= ((EntityProvider<T>) (_provider ?? transform.LocalProvider<T>())).Instance;

        protected virtual void OnEnable() => EntityController().Add(this);
        protected virtual void OnDisable() => EntityController().Remove(this);
    }
}
