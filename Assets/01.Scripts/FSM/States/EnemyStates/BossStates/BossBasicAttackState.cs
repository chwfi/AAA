using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBasicAttackState : BossGroundedState
{
    [SerializeField] private float _dashDelay;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _cooldown;

    public override void EnterState()
    {
        base.EnterState();

        Boss.BossSkillController.IsPlayingSkill = true;
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
        Boss.AnimatorCompo.InitAnimation();
        Boss.BossSkillController.IsPlayingSkill = false;
    }
}
