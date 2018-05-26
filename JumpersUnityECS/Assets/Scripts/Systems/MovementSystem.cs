using Components;
using Unity.Entities;
using UnityEngine;

namespace Systems
{
    [UpdateAfter(typeof(InputUpdateGroup))]
    public class MovementSystem : ComponentSystem
    {
        struct Group
        {
            public Transform Transform;
            public MovementInput MovementInput;
        }

        protected override void OnUpdate()
        {
            var deltaTime = Time.deltaTime;

            foreach(var entity in this.GetEntities<Group>())
            {
                var pos = entity.Transform.position;

                pos.x = pos.x + entity.MovementInput.X * entity.MovementInput.Config.MoveSpeedX * deltaTime;
                pos.y = pos.y + entity.MovementInput.Y * entity.MovementInput.Config.MoveSpeedY * deltaTime;

                entity.Transform.position = pos;
            }
        }
    }
}
