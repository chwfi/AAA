using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicAttackSkill : BossSkill
{
    private float _meleeAttackDistance;
    private float _coolDown;

    public BasicAttackSkill(BossController boss, Transform bossTransform, BossSkillController skillController, float meleeAttackDistance, float coolDown) 
        : base(boss, bossTransform, skillController)
    {
        _meleeAttackDistance = meleeAttackDistance;
        _coolDown = coolDown;
    }

    public override bool CanExecute()
    {
        if (_skillController.IsPlayingSkill) return false;

        return Vector3.Distance(TargetPos, BossTransform.position) < _meleeAttackDistance && CanExecuteSkill;
    }

    public override void Execute()
    {
        Boss.BossStateMachine.ChangeState(StateTypeEnum.BasicAttack);
        SetCoolDown();
    }

    public override void SetCoolDown()
    {
        CanExecuteSkill = false;
        CoroutineUtil.CallWaitForSeconds(_coolDown, () => CanExecuteSkill = true);
    }
}
