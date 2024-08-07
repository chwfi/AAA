using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [Header("Target")]
    public Collider CurrentTarget;
    public LayerMask TargetLayer;
    public float targetRange;
    public float stopRange;

    protected Entity _owner;
    private readonly Collider[] _hitColliders = new Collider[10];

    protected virtual void Awake()
    {

    }

    protected virtual void Update()
    {
        Targeting();
    }

    public void Targeting()
    {
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, targetRange, _hitColliders, TargetLayer);

        Collider closestEnemyColl = null;
        float closestDistance = float.MaxValue;

        for (int i = 0; i < numColliders; i++)
        {
            float distance = Vector3.Distance(transform.position, _hitColliders[i].ClosestPoint(transform.position));
            if (distance < targetRange && distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemyColl = _hitColliders[i];
            }
        }

        CurrentTarget = closestEnemyColl;
    }

    public bool IsTargetInRange(float range)
    {
        if (CurrentTarget == null) return false;
        else return Vector3.Distance(transform.position, CurrentTarget.transform.position) <= range;
    }

    public void RotateToTarget()
    {
        if (CurrentTarget == null) return;

        Vector3 dir = new(CurrentTarget.ClosestPoint(transform.position).x, transform.position.y,
            CurrentTarget.ClosestPoint(transform.position).z);
        var destRotation = Quaternion.LookRotation(dir - transform.position);
        transform.rotation = destRotation;
    }

    public virtual void AttackTrigger()
    {
        
    }
}
