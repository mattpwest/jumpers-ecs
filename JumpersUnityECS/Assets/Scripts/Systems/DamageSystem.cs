using Components;
using Unity.Entities;
using UnityEngine;

namespace Systems
{
    [UpdateAfter(typeof(InputUpdateGroup))]
    public class DamageSystem : ComponentSystem
    {
        struct DamageGroup
        {
            public ComponentArray<Health> Health;
            public ComponentDataArray<Damage> Damage;
            public EntityArray Entities;
            public int Length;
        }
        
        struct Group
        {
            public Health Health;
            public Damage Damage;
        }
        
        [Inject]
        private DamageGroup group;

        protected override void OnUpdate()
        {
            for(int i = 0; i < this.@group.Length; i++)
            {
                Debug.Log("Doing damage: " + this.@group.Damage[i].HitPoints);
                
                this.@group.Health[i].HitPoints -= this.@group.Damage[i].HitPoints;
                this.PostUpdateCommands.RemoveComponent<Damage>(this.@group.Entities[i]);
            }
        }
    }
}
