using UnityEngine;

public interface IDamageable
{
    public void Hurt(float damageAmount);
    public void Heal(float healAmount);
    public void Die();
}
