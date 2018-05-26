using Components;
using Unity.Entities;
using UnityEngine;

namespace Systems
{
    [UpdateAfter(typeof(InputUpdateGroup))]
    public class RotatorSystem : ComponentSystem
    {
        struct Group
        {
            public Transform Transform;
            public Rotation Rotation;
            
        }

        protected override void OnUpdate()
        {
            var deltaTime = Time.deltaTime;

            foreach(var entity in this.GetEntities<Group>())
            {
                entity.Transform.Rotate(Vector3.forward, entity.Rotation.DegreesPerSecond * deltaTime);
            }
        }
    }
}
