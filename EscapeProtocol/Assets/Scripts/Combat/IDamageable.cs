namespace Combat
{
    public interface IDamageable
    {
        public int Health { get; }
        public bool IsDead { get; }
        void TakeDamage(int damage);
    }
}
