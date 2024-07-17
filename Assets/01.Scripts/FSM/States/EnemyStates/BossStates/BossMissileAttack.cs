using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissileAttack : BossGroundedState
{
    [SerializeField] private float _cooldown;

    public override void EnterState()
    {
        base.EnterState();

        Boss.AnimatorCompo.SetMissileAttackAnimation(true);
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void ExitState()
    {
        base.ExitState();
        Boss.AnimatorCompo.SetMissileAttackAnimation(false);
        Boss.SkillPatternCompo.SetCoolDownHandler(_cooldown, BossAttackType.MissileAttack);
    }
}
