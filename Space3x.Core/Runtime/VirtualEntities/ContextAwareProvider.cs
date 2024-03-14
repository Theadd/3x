using System;
using Space3x.Core.VirtualEntities;
using UnityEngine.UIElements;

namespace Space3x.Core.Config
{
    public interface IEventArgs { }
    
    public interface IEventRegistry // TODO: rename
    {
        /// <summary>
        /// Gets triggered when the state of this entity changes.
        /// </summary>
        public event Action<StateChangeEvent> OnStateChange;
        
        /// <summary>
        /// Gets or sets the current state of this entity,
        /// propagating the OnStateChange event.
        /// </summary>
        public EntityState State { get; set; }
    }
    
    public enum EntityState
    {
        /// <summary>
        /// Player (or game) is still loading and all inputs are ignored.
        /// </summary>
        Joining,
        /// <summary>
        /// Playing.
        /// </summary>
        Active,
        /// <summary>
        /// Remaining cases, in which it has no direct access to input
        /// actions on the player controller but it might have on the UI.
        /// </summary>
        Standby,
        /// <summary>
        /// Player not enabled, inactive or not in play mode.
        /// </summary>
        Inactive
    }
    
    public sealed class ChangeEvent : IEventRegistry
    {
        /// <summary>
        /// Gets or sets the current state of this entity,
        /// propagating the OnStateChange event.
        /// </summary>
        public EntityState State { get; set; } = EntityState.Joining;

        /// <summary>
        /// Gets triggered when the state of this entity changes.
        /// </summary>
        public event Action<StateChangeEvent> OnStateChange;
        
        public void NotifyStateChange(EntityState state) => 
            OnStateChange?.Invoke(new StateChangeEvent { State = State = state });
    }
    
    public class StateChangeEvent : IEventArgs
    {
        public EntityState State { get; set; }
    }
    
    public abstract class ContextAwareProvider : ContextProvider, IEventRegistry
    {
        private readonly IEventRegistry _state = new ChangeEvent();
        
        /// <summary>
        /// Gets or sets the current state of this entity,
        /// propagating the OnStateChange event.
        /// </summary>
        public EntityState State
        {
            get => _state.State;
            set => (_state as ChangeEvent)?.NotifyStateChange(value);
        }
        
        /// <summary>
        /// Gets triggered when the player state changes.
        /// </summary>
        public event Action<StateChangeEvent> OnStateChange
        {
            add => _state.OnStateChange += value;
            remove => _state.OnStateChange -= value;
        }
    }
}
