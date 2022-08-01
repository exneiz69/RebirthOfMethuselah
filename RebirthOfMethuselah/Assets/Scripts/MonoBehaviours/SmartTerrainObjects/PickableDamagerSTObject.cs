using UnityEngine;

public class PickableDamagerSTObject : PickableSTObject, IDamager
{
    public bool CanDamage { get; private set; } = true;

    public void Damage(IDamageable target)
    {
        if (target is null)
        {
            throw new System.ArgumentNullException(nameof(target));
        }
        else if (!CanDamage)
        {
            throw new System.InvalidOperationException();
        }
        else
        {
            target.Damage();
        }
    }
}
