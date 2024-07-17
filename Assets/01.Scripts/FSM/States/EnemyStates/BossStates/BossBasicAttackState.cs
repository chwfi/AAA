using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBasicAttackState : BossBaseState
{
    [SerializeField] private float _dashDelay;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _cooldown;

    private int _count = 0;

    public override void EnterState()
    {
        base.EnterState();

        _count++;
        Boss.AnimatorCompo.SetAttackTrigger();
        Boss.AttackCompo.RotateToTarget();
        Boss.MoveCompo.Dash(transform.forward, _dashDelay, _dashTime, _dashSpeed, DashTypeEnum.AttackDash);
        Boss.AnimatorCompo.SetBasicAttackAnimation(true);
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void ExitState()
    {
        base.ExitState();

        if (_count > 1)
        {
            Boss.SkillPatternCompo.SetCoolDownHandler(_cooldown, BossAttackType.BasicAttack);
            _count = 0;
        }
    }
}
