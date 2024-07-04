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

        if (_owner.InputReader.MoveInput.x < 0)
        {
            _owner.Dash(-_owner.AnimatorCompo.transform.right, 
                _dodgeDelay, _dodgeTime, _dodgeSpeed + 0.1f, DashTypeEnum.DodgeDash, true);
            _owner.AnimatorCompo.SetDodgeLeftAnimation(true);
        }
        else if (_owner.InputReader.MoveInput.x > 0)
        {
            _owner.Dash(_owner.AnimatorCompo.transform.right, 
                _dodgeDelay, _dodgeTime, _dodgeSpeed + 0.1f, DashTypeEnum.DodgeDash, true);
            _owner.AnimatorCompo.SetDodgeRightAnimation(true);
        }
        else if (_owner.InputReader.MoveInput.y == -1)
        {
            _owner.Dash(-_owner.AnimatorCompo.transform.forward,
                _dodgeDelay, _dodgeTime, _dodgeSpeed - 0.1f, DashTypeEnum.DodgeDash, true);
            _owner.AnimatorCompo.SetDodgeBackAnimation(true);
        }
        else
        {
            _owner.Dash(_owner.AnimatorCompo.transform.forward,
                _dodgeDelay, _dodgeTime, _dodgeSpeed, DashTypeEnum.DodgeDash, true);
            _owner.AnimatorCompo.SetDodgeAnimation(true);
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
