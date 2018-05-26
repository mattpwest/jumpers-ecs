using Components;
using Rewired;
using Unity.Entities;
using Player = Components.Player;

namespace Systems
{
	[UpdateInGroup(typeof(InputUpdateGroup))]
	public class PlayerInputSystem : ComponentSystem
	{

		struct Group
		{
			public Player Player;
			public MovementInput MovementInput;
		}

		protected override void OnUpdate()
		{
			var input = ReInput.players.GetPlayer(0);
			var x = input.GetAxis("Move Horizontal");
			var y = input.GetAxis("Move Vertical");

			foreach(var entity in this.GetEntities<Group>())
			{
				entity.MovementInput.X = x;
				entity.MovementInput.Y = y;
			}
		}
	}
}