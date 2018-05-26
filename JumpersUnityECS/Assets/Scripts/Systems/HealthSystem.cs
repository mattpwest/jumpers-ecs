using Components;
using Unity.Entities;

namespace Systems
{
    [UpdateAfter(typeof(DamageSystem))]
    public class HealthSystem : ComponentSystem
    {
        struct Group
        {
            public ComponentArray<Health> Health;
            public EntityArray Entities;
            public int Length;
        }

        [Inject]
        private Group group;

        protected override void OnUpdate()
        {
            for(int i = 0; i < this.group.Length; i++)
            {
                if(this.group.Health[i].HitPoints <= 0)
                {
                    this.EntityManager.AddComponentData(this.group.Entities[i], new Destroy());
                }
            }
        }
    }
}
