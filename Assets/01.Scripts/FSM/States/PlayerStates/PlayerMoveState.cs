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

        Player.MoveCompo.MoveSpeed =
            Player.InputReader.Sprint ? Player.MoveCompo.SprintSpeed : 2;

        if (Player.InputReader.MoveInput.magnitude <= 0)
        {
            _stateMachine.ChangeState(StateTypeEnum.Idle);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
