using System;

namespace Data.ValueObjects
{
    [Serializable]
    public struct GunData
    {
        public float BaseAttackDamage;
        public float HeavyAttackDamage;
        public float BaseAttackFireRate;
        public float HeavyAttackFireRate;
    }
}
