using Components;
using Rewired;
using Unity.Entities;
using UnityEngine;
using Player = Components.Player;

namespace Systems
{
	[UpdateInGroup(typeof(InputUpdateGroup))]
	public class ShootSystem : ComponentSystem
	{
		struct Group
		{
			public ShootInput ShootInput;
			public Shooter Shooter;
		}

		protected override void OnUpdate()
		{
			var deltaTime = Time.deltaTime;
			
			foreach(var entity in this.GetEntities<Group>())
			{
				entity.Shooter.charge += deltaTime;
				
				if(entity.ShootInput.Fire && entity.Shooter.charge >= entity.Shooter.chargeTime)
				{
					foreach(Transform spawnPoint in entity.Shooter.BulletSpawns)
					{
						GameObject.Instantiate(entity.Shooter.BulletPrefab, spawnPoint.position, Quaternion.identity);
					}

					entity.Shooter.charge = 0;
				}
			}
		}
	}
}