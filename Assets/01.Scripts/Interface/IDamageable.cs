using UnityEngine;

public interface IDamageable
{
    public void ApplyDamage(int damage, Vector3 point, Vector3 normal, HitTypeEnum hitType);
}