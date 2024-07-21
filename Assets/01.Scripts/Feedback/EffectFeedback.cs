using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectFeedback : Feedback
{
    [SerializeField] private EffectController _effect;

    public override void SetOwner(Entity owner)
    {
        base.SetOwner(owner);
    }

    public override void StartFeedback()
    {
        EffectController effect = PoolManager.Instance.Pop($"{_effect.name}") as EffectController;
        effect.transform.position = _owner.AttackCompo.CurrentTarget.transform.position;
        effect.StartPlay();
    }
}
