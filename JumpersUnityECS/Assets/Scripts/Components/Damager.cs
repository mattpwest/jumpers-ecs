using Unity.Entities;
using UnityEngine;

namespace Components
{
    public class Damager : MonoBehaviour
    {
        public int HitPoints;
        public DamageType Type;

        private void OnTriggerEnter2D(Collider2D other)
        {
            
            // var entity = gameObjectEntity.Entity;
            // var manager = gameObjectEntity.EntityManager;
            //
            // manager.AddComponent();
            // manager.AddComponent(entity, typeof(Damage));
            // manager.GetComponentData<Damage>(entity);

            // var damage = other.gameObject.AddComponent<Damage>();
            // damage.HitPoints = this.HitPoints;
            // damage.Type = this.Type;

            var gameObjectEntity  = other.GetComponent<GameObjectEntity>();
            gameObjectEntity.EntityManager.AddComponent(gameObjectEntity.Entity, typeof(Damage));

            var data = gameObjectEntity.EntityManager.GetComponentData<Damage>(gameObjectEntity.Entity);
            data.HitPoints = this.HitPoints;
            data.Type = this.Type;
            
            gameObjectEntity.EntityManager.SetComponentData(gameObjectEntity.Entity, data);
        }
    }
}