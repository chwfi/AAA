using UnityEngine;

public class BossIdleState : BossGroundedState
{
    public override void EnterState()
    {
        base.EnterState();

        Boss.MoveCompo.MoveSpeed = 0;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (Boss.BossAttackCompo.IsTargetInRange(_chaseRange))
        {
            _stateMachine.ChangeState(StateTypeEnum.Move);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}