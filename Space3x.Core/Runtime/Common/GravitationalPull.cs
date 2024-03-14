using Space3x.Core.Collections;
using UnityEngine;

namespace Space3x.Core.Runtime
{
    public interface IGravityPull { }
    public readonly struct GravitationalPull
    {
        public Vector3 Direction { get; }
        public Transform Origin { get; }
        public float Mass { get; }
        public float Radius { get; }
        public bool IsWorldUp { get; }
    }
    
    public class GravitationalPuller : IGravityPull
    {
        public RefList<GravitationalPull> items = new RefList<GravitationalPull>();

        public float GetForce { get; private set; } = 0f;

        public Vector3 GetDirection { get; private set; } = Vector3.up;

        public void Add(ref GravitationalPull pull)
        {
            if (IndexOf(ref pull) >= 0)
                items.Add(pull);
        }
        
        public void Remove(ref GravitationalPull pull)
        {
            var index = IndexOf(ref pull);
            if (index >= 0) 
                items.RemoveAt(index);
        }

        private int IndexOf(ref GravitationalPull pull)
        {
            for (var i = 0; i < items.Count; i++)
            {
                ref var item = ref items.Get(i);
                if ((pull.IsWorldUp && item.Direction == pull.Direction && Mathf.Approximately(item.Mass, pull.Mass)) ||
                    (!pull.IsWorldUp && item.Origin == pull.Origin))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Apply(Vector3 targetPosition)
        {
            GetForce = 0f;
            GetDirection = Vector3.zero;
            
            for (var i = 0; i < items.Count; i++)
            {
                ref var item = ref items.Get(i);
                var direction = targetPosition - item.Origin.position;
                var distance = direction.magnitude;
                var force = item.Mass * CalculateGravityForce(item.Mass, distance, item.Radius);
                // get the mean direction and resulting force to be applied
                GetForce += force;
                GetDirection += direction.normalized * force;
            }
        }
        
        private static float CalculateGravityForce(float mass, float distance, float radius)
        {
            float gravitationalConstant = 6.67430e-11f;
            return (gravitationalConstant * mass) / (distance - radius) * (distance - radius);
        }
    }
    
    
    
}
