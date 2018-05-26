using System.Collections.Generic;
using Components;
using Unity.Entities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Systems
{
    [UpdateAfter(typeof(DamageSystem))]
    public class SpawnSystem : ComponentSystem
    {
        public int NUM_ENEMIES = 100;
        
        struct PlayerSpawns
        {
            public ComponentArray<Spawn> Spawns;
            public ComponentArray<Player> Players;
            public int Length;
        }

        struct EnemySpawns
        {
            public ComponentArray<Spawn> Spawns;
            public ComponentArray<Enemy> Enemies;
            public int Length;
        }

        struct PlayersGroup
        {
            public ComponentArray<Player> Players;
            public SubtractiveComponent<Spawn> Spawn;
            public int Length;
        }

        struct EnemiesGroup
        {
            public ComponentArray<Enemy> Enemies;
            public SubtractiveComponent<Spawn> Spawn;
            public int Length;
        }

        [Inject]
        private PlayerSpawns playerSpawns;

        [Inject]
        private PlayersGroup players;

        [Inject]
        private EnemySpawns enemySpawns;

        [Inject]
        private EnemiesGroup enemies;

        protected override void OnUpdate()
        {
            var deltaTime = Time.deltaTime;

            int playersSpawned = UpdateSpawners(this.playerSpawns.Spawns, deltaTime);
            int enemiesSpawned = UpdateSpawners(this.enemySpawns.Spawns, deltaTime);
            
            if(this.players.Length + playersSpawned < 1)
            {
                this.Spawn(this.playerSpawns.Spawns, this.players.Length + playersSpawned, 1);
            }
            
            if(this.enemies.Length + enemiesSpawned < NUM_ENEMIES)
            {
                this.Spawn(this.enemySpawns.Spawns, this.enemies.Length + enemiesSpawned, this.NUM_ENEMIES);
            }
        }
        
        private void Spawn(ComponentArray<Spawn> spawns, int count, int target)
        {
            List<Spawn> inactiveSpawns = new List<Spawn>();
            
            for(int i = 0; i < spawns.Length; i++)
            {
                var spawn = spawns[i];
                if(spawn.spawning)
                {
                    count++;
                }
                else
                {
                    inactiveSpawns.Add(spawn);
                }
            }

            while(count < target)
            {
                var index = Random.Range(0, inactiveSpawns.Count);
                var spawn = inactiveSpawns[index];
                inactiveSpawns.RemoveAt(index);
                spawn.TimeLeft = spawn.TimeToTrigger;
                spawn.spawning = true;
                count++;
            }
        }
        
        private int UpdateSpawners(ComponentArray<Spawn> spawns, float deltaTime)
        {
            int spawnCount = 0;
            for(int i = 0; i < spawns.Length; i++)
            {
                var spawn = spawns[i];

                if(spawn.TimeLeft > 0.0)
                {
                    spawn.TimeLeft -= deltaTime;
                }
                else if(spawn.TimeLeft < 0.0)
                {
                    GameObject.Instantiate(spawn.Prefab, spawn.gameObject.transform.position, Quaternion.identity);
                    spawn.TimeLeft = 0.0f;
                    spawn.spawning = false;
                    spawnCount++;
                }
            }

            return spawnCount;
        }
    }
}
