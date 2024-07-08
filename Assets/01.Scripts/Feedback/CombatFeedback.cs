using UnityEngine;

public class CombatFeedback : Feedback, IHitFeedbackable
{
    [SerializeField] private ParticleSystem _hitEffect;

    public void ApplyFeedback(Vector3 hitPoint)
    {
        _hitEffect.transform.position = hitPoint;
        _hitEffect.Play();
    }
}
