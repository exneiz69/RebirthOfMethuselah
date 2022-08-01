public interface IDamager
{
    public bool CanDamage { get; }

    public void Damage(IDamageable target);
}