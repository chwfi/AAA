using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : State
{
    [SerializeField] private float _dodgeDelay;
    [SerializeField] private float _dodgeTime;
    [SerializeField] private float _dodgeSpeed;

    public override void EnterState()
    {
        base.EnterState();

        _owner.StopImmediately();

        _owner.Dash(_owner.AnimatorCompo.transform.forward, _dodgeDelay, _dodgeTime, _dodgeSpeed);

        _owner.AnimatorCompo.SetDodgeAnimation(true);
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
