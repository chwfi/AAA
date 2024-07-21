using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissileAttackState : BossGroundedState
{
    [SerializeField] private float _cooldown;

    public override void EnterState()
    {
        base.EnterState();

        Boss.BossSkillController.IsPlayingSkill = true;
        Boss.AnimatorCompo.SetMissileAttackAnimation(true);
        _owner.FeedbackDictionary.TryGetValue(FeedbackTypeEnum.Effect, out Feedback feedback);
        feedback.StartFeedback();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void ExitState()
    {
        base.ExitState();
        Boss.BossSkillController.IsPlayingSkill = false;
        Boss.AnimatorCompo.SetMissileAttackAnimation(false);
    }
}
