using System.Collections;
using UnityEngine;

public enum BossAttackType
{
    BasicAttack,
    JumpAttack,
}

public class BossSkillPattern : MonoBehaviour, IBossable
{
    [Header("Condition Value")]
    [SerializeField] private float _jumpAttackDistance;
    [SerializeField] private float _meleeAttackDistance;

    public bool CanBasicAttack;
    public bool CanJumpAttack;

    public BossController Boss { get; set; }

    private Coroutine CooldownCoroutine;
    private Vector3 TargetPos => Boss.AttackCompo.CurrentTarget.ClosestPoint(transform.position);

    public void SetOwner(BossController boss)
    {
        Boss = boss;
        CanBasicAttack = true;
        CanJumpAttack = true;
    }

    private void Update()
    {
        if (Boss.AttackCompo.CurrentTarget == null) return;

        if (Vector3.Distance(TargetPos, transform.position) > _jumpAttackDistance && CanJumpAttack && CanBasicAttack)
        {
            Boss.BossStateMachine.ChangeState(StateTypeEnum.JumpAttack);
            CanJumpAttack = false;  
        }

        if (Vector3.Distance(TargetPos, transform.position) < _meleeAttackDistance && CanBasicAttack && CanJumpAttack)
        {
            Boss.BossStateMachine.ChangeState(StateTypeEnum.BasicAttack);
            CanBasicAttack = false;
        }
    }

    public void SetCoolDownHandler(float coolTime, BossAttackType attackType)
    {
        if (CooldownCoroutine != null)
            StopCoroutine(CooldownCoroutine);

        CooldownCoroutine = StartCoroutine(SetCoolDown(coolTime, attackType));
    }

    private IEnumerator SetCoolDown(float coolTime, BossAttackType attackType)
    {
        yield return new WaitForSeconds(coolTime);

        switch (attackType)
        {
            case BossAttackType.BasicAttack:
                CanBasicAttack = true;
                break;
            case BossAttackType.JumpAttack:
                CanJumpAttack = true;
                break;
            default:
                break;
        }
    }
}
