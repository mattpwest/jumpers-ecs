using Components;
using Unity.Entities;
using UnityEngine;

namespace Systems
{
    [UpdateInGroup(typeof(InputUpdateGroup))]
    public class EnemyFlyInputSystem : ComponentSystem
    {
        struct Group
        {
            public Enemy Enemy;
            public MovementInput MovementInput;
            public Transform Transform;
        }

        protected override void OnUpdate()
        {
            foreach(var entity in this.GetEntities<Group>())
            {
                entity.MovementInput.Y = -1;
                entity.MovementInput.X = Mathf.PerlinNoise(entity.Transform.position.x, entity.Transform.position.y) - 0.5f;
            }
        }
    }
}