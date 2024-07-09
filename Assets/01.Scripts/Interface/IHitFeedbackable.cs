using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitFeedbackable
{
    public void ApplyFeedback(Vector3 hitPoint, HitTypeEnum hitType);
    public void ApplyCameraShake();
}
