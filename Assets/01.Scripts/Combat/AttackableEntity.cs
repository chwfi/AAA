using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableEntity : MonoBehaviour
{
    public Entity CurrentTarget;
    public LayerMask TargetLayer;
    public float targetRange;
    public float stopRange;

    public DamageCaster DamageCasterCompo { get; private set; }
    protected Entity _owner;
    private readonly Collider[] _hitColliders = new Collider[10];
    protected Collider enemyColl;

    protected virtual void Awake()
    {
        DamageCasterCompo = GetComponent<DamageCaster>();
    }

    protected virtual void Update()
    {
        Targeting();
    }

    public void Targeting()
    {
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, 15f, _hitColliders, TargetLayer);
        Enemy closestEnemy = null;

        for (int i = 0; i < numColliders; i++)
        {
            enemyColl = _hitColliders[i];
            if (enemyColl != null)
            {
                float distance = Vector3.Distance(transform.position,
                                        enemyColl.ClosestPoint(transform.position));
                if (distance < targetRange)
                {
                    closestEnemy = enemyColl.GetComponentInParent<Enemy>();
                }
            }
        }

        if (closestEnemy != null)
        {
            float minDistance = Vector3.Distance(transform.position, closestEnemy.transform.position);

            if (minDistance > stopRange)
            {
                CurrentTarget = closestEnemy;
            }
        }
        else
        {
            CurrentTarget = null;
        }
    }

    public virtual void AttackTrigger()
    {
        DamageCasterCompo.CastDamage(TargetLayer);
    }
}
