using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public override void EnterState()
    {
        base.EnterState();

        _owner.MoveSpeed = 0;
        _owner.InitAttackCombo();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_owner.InputReader.MoveInput.magnitude > 0)
        {
            _stateMachine.ChangeState(StateTypeEnum.Move);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
