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

        if (Player.InputReader.MoveInput.x < 0)
        {
            Player.MoveCompo.Dash(-Player.AnimatorCompo.transform.right, 
                _dodgeDelay, _dodgeTime, _dodgeSpeed + 0.1f, DashTypeEnum.DodgeDash, true);
            Player.AnimatorCompo.SetDodgeLeftAnimation(true);
        }
        else if (Player.InputReader.MoveInput.x > 0)
        {
            Player.MoveCompo.Dash(Player.AnimatorCompo.transform.right, 
                _dodgeDelay, _dodgeTime, _dodgeSpeed + 0.1f, DashTypeEnum.DodgeDash, true);
            Player.AnimatorCompo.SetDodgeRightAnimation(true);
        }
        else if (Player.InputReader.MoveInput.y == -1)
        {
            Player.MoveCompo.Dash(-Player.AnimatorCompo.transform.forward,
                _dodgeDelay, _dodgeTime, _dodgeSpeed - 0.1f, DashTypeEnum.DodgeDash, true);
            Player.AnimatorCompo.SetDodgeBackAnimation(true);
        }
        else
        {
            Player.MoveCompo.Dash(Player.AnimatorCompo.transform.forward,
                _dodgeDelay, _dodgeTime, _dodgeSpeed, DashTypeEnum.DodgeDash, true);
            Player.AnimatorCompo.SetDodgeAnimation(true);
        }
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
