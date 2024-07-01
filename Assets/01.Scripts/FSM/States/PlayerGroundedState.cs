using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : State
{
    public override void EnterState()
    {
        base.EnterState();

        _owner.InputReader.BasicAttackEvent += AttackHandle;
        _owner.InputReader.DodgeEvent += DodgeHandle;
    }

    public override void ExitState()
    {
        base.ExitState();

        _owner.InputReader.BasicAttackEvent -= AttackHandle;
        _owner.InputReader.DodgeEvent -= DodgeHandle;
    }

    private void AttackHandle()
    {
        if (_owner.CanAttack)
            _stateMachine.ChangeState(StateTypeEnum.BasicAttack);
    }

    private void DodgeHandle()
    {
        if (_owner.CanAttack)
            _stateMachine.ChangeState(StateTypeEnum.Dodge);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _owner.Move();
    }
}
