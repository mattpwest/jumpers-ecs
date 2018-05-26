using Components;
using Unity.Entities;
using UnityEngine;

namespace Systems
{
    [UpdateAfter(typeof(HealthSystem))]
    public class DestroySystem : ComponentSystem
    {
        struct Group
        {
            public ComponentArray<Transform> Transforms;
            public ComponentDataArray<Destroy> Destroys;
            public GameObjectArray GameObjects;
            public int Length;
        }
        
        [Inject]
        private Group group;

        protected override void OnUpdate()
        {
            for (int i = 0; i < this.@group.Length; i++)
            {
                GameObject.Destroy(this.@group.GameObjects[i]);
            }
        }
    }
}
