using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    [SerializeField] private float _dodgeDelay;
    [SerializeField] private float _dodgeTime;
    [SerializeField] private float _dodgeSpeed;

    public override void EnterState()
    {
        base.EnterState();

        Player.MoveCompo.StopImmediately();

        Player.MoveCompo.Dash(Player.AnimatorCompo.transform.forward,
                _dodgeDelay, _dodgeTime, _dodgeSpeed, DashTypeEnum.DodgeDash, true);
        Player.AnimatorCompo.SetDodgeAnimation(true);
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
