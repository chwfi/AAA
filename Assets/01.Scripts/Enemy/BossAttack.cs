using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : TargetController, IBossable
{
    public BossController Boss { get; set; }

    private Transform targetTrm;

    public void SetOwner(BossController enemy)
    {
        Boss = enemy;
    }

    protected override void Update()
    {
        base.Update();

        if (CurrentTarget != null && targetTrm == null)
        {
            targetTrm = CurrentTarget.GetComponentInParent<Entity>().transform;
        }
    }

    public void RotateToPlayer()
    {
        if (CurrentTarget == null) return;

        Vector3 dir = new(targetTrm.position.x, transform.position.y, targetTrm.position.z);
        var destRotation = Quaternion.LookRotation(dir - transform.position);
        transform.rotation = destRotation;
    }
}
