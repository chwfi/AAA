using Unity.VisualScripting;
using UnityEngine;

public class JumpAttackSkill : BossSkill
{
    private float _jumpAttackDistance;
    private float _coolDown;

    public JumpAttackSkill(BossController boss, Transform bossTransform, BossSkillController skillController, float jumpAttackDistance, float coolDown) 
        : base(boss, bossTransform, skillController)
    {
        _jumpAttackDistance = jumpAttackDistance;
        _coolDown = coolDown;
    }

    public override bool CanExecute()
    {
        if (_skillController.IsPlayingSkill) return false;

        return Vector3.Distance(TargetPos, BossTransform.position) > _jumpAttackDistance && CanExecuteSkill;
    }

    public override void Execute()
    {
        Boss.BossStateMachine.ChangeState(StateTypeEnum.JumpAttack);
        SetCoolDown();
    }

    public override void SetCoolDown()
    {
        CanExecuteSkill = false;
        CoroutineUtil.CallWaitForSeconds(_coolDown, () => CanExecuteSkill = true);
    }
}
