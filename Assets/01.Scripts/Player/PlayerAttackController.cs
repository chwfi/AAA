using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public TestEnemy CurrentTarget;
    public DamageCaster DamageCasterCompo { get; private set; }

    [SerializeField] private float _targetRange;
    [SerializeField] private float _stopRange;
    [SerializeField] private LayerMask _enemyLayer;

    private Collider[] _hitColliders = new Collider[10];

    private void Awake()
    {
        DamageCasterCompo = GetComponent<DamageCaster>();
    }

    private void Update()
    {
        SetTarget();
    }

    public void SetTarget()
    {
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, 15f, _hitColliders, _enemyLayer);
        TestEnemy closestEnemy = null;

        for (int i = 0; i < numColliders; i++)
        {
            Collider enemyColl = _hitColliders[i];
            if (enemyColl != null)
            {
                float distance = Vector3.Distance(transform.position,
                                        enemyColl.ClosestPoint(transform.position));
                if (distance < _targetRange)
                {
                    closestEnemy = enemyColl.GetComponent<TestEnemy>();
                }
            }
        }

        if (closestEnemy != null)
        {
            float minDistance = Vector3.Distance(transform.position, closestEnemy.transform.position);

            if (minDistance > _stopRange)
            {
                CurrentTarget = closestEnemy;
            }
        }
        else
        {
            CurrentTarget = null;
        }
    }

    public bool IsTargetInStopRange()
    {
        if (CurrentTarget == null) return false;

        if (Vector3.Distance(transform.position, CurrentTarget.transform.position) < _stopRange) return true;
        else return false;
    }

    public void RotateToTarget()
    {
        var destRotation = Quaternion.LookRotation(CurrentTarget.transform.position - transform.position);
        transform.rotation = destRotation;
    }

    public void AttackTrigger()
    {
        DamageCasterCompo.CastDamage(_enemyLayer);
    }
}
