using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAttackSkill : BossSkill
{
    private float _missileAttackDistance;
    private float _coolDown;

    public MissileAttackSkill(BossController boss, Transform bossTransform, BossSkillController skillController, float missileAttackDistance, float coolDown) 
        : base(boss, bossTransform, skillController)
    {
        _missileAttackDistance = missileAttackDistance;
        _coolDown = coolDown;
    }

    public override bool CanExecute()
    {
        if (_skillController.IsPlayingSkill) return false;

        return Vector3.Distance(TargetPos, BossTransform.position) < _missileAttackDistance && CanExecuteSkill;
    }

    public override void Execute()
    {
        Boss.BossStateMachine.ChangeState(StateTypeEnum.MissileAttack);
        SetCoolDown();
    }

    public override void SetCoolDown()
    {
        CanExecuteSkill = false;
        CoroutineUtil.CallWaitForSeconds(_coolDown, () => CanExecuteSkill = true);
    }
}
