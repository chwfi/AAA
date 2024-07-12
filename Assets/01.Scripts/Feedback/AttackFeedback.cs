using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFeedback : Feedback
{
    [SerializeField] private ParticleSystem[] _swordTrail;
    [SerializeField] private AudioSource _audio;

    public override void SetOwner(Entity owner)
    {
        base.SetOwner(owner);
    }

    public override void StartFeedback()
    {
        ApplyAttackEffect();
        ApplyAttackSound();
    }

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
