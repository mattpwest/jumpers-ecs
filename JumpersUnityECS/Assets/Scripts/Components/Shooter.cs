using UnityEngine;

namespace Components
{
    public class Shooter : MonoBehaviour
    {
        public Transform BulletPrefab;
        public Transform[] BulletSpawns;
        public float charge;
        public float chargeTime;
    }
}