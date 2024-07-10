using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFeedback : Feedback, IAttackFeedbackable
{
    [SerializeField] private ParticleSystem[] _swordTrail;

    public void ApplyAttackEffect()
    {
        foreach (var effect in _swordTrail)
        {
            effect.Play();
        }
    }
}
