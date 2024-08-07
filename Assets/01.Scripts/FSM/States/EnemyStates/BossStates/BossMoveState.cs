public class BossMoveState : BossGroundedState
{
    public override void EnterState()
    {
        base.EnterState();

        Boss.MoveCompo.MoveSpeed = 1.45f;
        Boss.AnimatorCompo.SetWalkAnimation(true);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Boss.MoveCompo.Move();

        if (!Boss.BossAttackCompo.IsTargetInRange(_chaseRange))
        {
            _stateMachine.ChangeState(StateTypeEnum.Idle);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        Boss.AnimatorCompo.SetWalkAnimation(false);
    }
}
