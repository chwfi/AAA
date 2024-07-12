using UnityEngine;

public class HitFeedback : Feedback
{
    public override void SetOwner(Entity owner)
    {
        base.SetOwner(owner);
    }

    public override void StartFeedback()
    {
        ApplyHitEffectFeedback(_owner.HealthCompo.hitPoint, _owner.HealthCompo.hitType);
    }

    public void ApplyCameraShake()
    {

    }

    public void ApplyHitEffectFeedback(Vector3 hitPoint, HitTypeEnum hitType)
    {
        EffectController effect = PoolManager.Instance.Pop($"{hitType}HitEffect") as EffectController;
        effect.transform.position = hitPoint;
        effect.StartPlay();
    }
}
