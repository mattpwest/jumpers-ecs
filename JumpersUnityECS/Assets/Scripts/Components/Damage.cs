using Unity.Entities;

namespace Components
{
    public class DamageComponent : ComponentDataWrapper<Damage> {}
    
    public struct Damage : IComponentData
    {
        public int HitPoints;
        public DamageType Type;
    }
}

