using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public override void EnterState()
    {
        base.EnterState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _owner.MoveSpeed = _owner.InputReader.Sprint ? _owner.SprintSpeed : 2;

        if (_owner.InputReader.MoveInput.magnitude <= 0)
        {
            _stateMachine.ChangeState(StateTypeEnum.Idle);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
