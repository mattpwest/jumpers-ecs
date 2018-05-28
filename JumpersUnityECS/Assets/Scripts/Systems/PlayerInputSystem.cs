#if !DISABLE_REWIRED
	using Rewired;
#endif

using Components;
using Unity.Entities;
using UnityEngine;
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
			public ShootInput ShootInput;
		}

		protected override void OnUpdate()
		{
			#if !DISABLE_REWIRED
				var input = ReInput.players.GetPlayer(0);
				var x = input.GetAxis("Move Horizontal");
				var y = input.GetAxis("Move Vertical");
				var fire = input.GetButton("Fire");
	        #else
				var x = 0.0f;
				if(Input.GetKey(KeyCode.A))
				{
					x = -1.0f;
				} else if(Input.GetKey(KeyCode.D))
				{
					x = 1.0f;
				}
			
				var y = 0.0f;
				if(Input.GetKey(KeyCode.W))
				{
					y = 1.0f;
				} else if(Input.GetKey(KeyCode.S))
				{
					y = -1.0f;
				}

				var fire = Input.GetKey(KeyCode.Space);
			#endif

			foreach(var entity in this.GetEntities<Group>())
			{
				entity.MovementInput.X = x;
				entity.MovementInput.Y = y;
				entity.ShootInput.Fire = fire;
			}
		}
	}
}