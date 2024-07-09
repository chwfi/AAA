using UnityEngine;

public class CombatFeedback : Feedback, IHitFeedbackable
{
    public void ApplyCameraShake()
    {

    }

    public void ApplyFeedback(Vector3 hitPoint, HitTypeEnum hitType)
    {
        EffectController effect = PoolManager.Instance.Pop($"{hitType}HitEffect") as EffectController;
        effect.transform.position = hitPoint;
        effect.StartPlay();
    }
}
