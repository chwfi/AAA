using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFeedback : Feedback, IAttackFeedbackable
{
    [SerializeField] private Transform _effectPos;

    public void ApplyAttackEffect(AttackTypeEnum attackType)
    {
        EffectController effect = PoolManager.Instance.Pop($"{attackType}AttackEffect") as EffectController;
        effect.transform.position = _effectPos.position;
        effect.StartPlay();
    }
}
