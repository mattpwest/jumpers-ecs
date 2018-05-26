using System.CodeDom;
using Systems;
using Unity.Entities;
using UnityEngine;

[UpdateAfter(typeof(InputUpdateGroup))]
public class Movement : ComponentSystem {

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
