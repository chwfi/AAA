using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossSkill
{
    protected BossController Boss;
    protected Transform BossTransform;
    protected Vector3 TargetPos => Boss.AttackCompo.CurrentTarget.ClosestPoint(BossTransform.position);
    protected BossSkillController _skillController;

    public bool CanExecuteSkill;

    public BossSkill(BossController boss, Transform bossTransform, BossSkillController skillController)
    {
        Boss = boss;
        BossTransform = bossTransform;
        _skillController = skillController;
        CanExecuteSkill = false;
    }

    public abstract bool CanExecute();
    public abstract void Execute();
    public abstract void SetCoolDown();
}
