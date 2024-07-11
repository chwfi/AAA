using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFeedback : Feedback, IAttackFeedbackable
{
    [SerializeField] private ParticleSystem[] _swordTrail;
    [SerializeField] private AudioSource _audio;

    public void ApplyAttackEffect()
    {
        foreach (var effect in _swordTrail)
        {
            effect.Play();
        }
    }

    public void ApplyAttackSound()
    {
        _audio.Play();
    }
}
