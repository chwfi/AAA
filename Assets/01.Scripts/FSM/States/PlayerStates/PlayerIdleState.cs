public class PlayerIdleState : PlayerGroundedState
{
    public override void EnterState()
    {
        base.EnterState();

        Player.MoveCompo.MoveSpeed = 0;
        Player.PlayerAttackCompo.InitAttackCombo();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (Player.InputReader.MoveInput.magnitude > 0)
        {
            _stateMachine.ChangeState(StateTypeEnum.Move);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
