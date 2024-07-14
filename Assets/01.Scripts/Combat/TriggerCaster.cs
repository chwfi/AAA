using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCaster : MonoBehaviour
{
    public LayerMask TargetLayer;

    private void OnTriggerEnter(Collider other)
    {
        if ((TargetLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);
            Health health = other.transform.GetComponentInParent<Health>();
            int damage = 10;

            health.ApplyDamage(damage, collisionPoint, Vector3.zero, HitTypeEnum.Spark);
        }
    }
}
