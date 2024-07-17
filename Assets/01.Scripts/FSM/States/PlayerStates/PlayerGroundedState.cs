using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public override void EnterState()
    {
        base.EnterState();

        Player.InputReader.BasicAttackEvent += AttackHandle;
        Player.InputReader.DodgeEvent += DodgeHandle;
    }

    public override void ExitState()
    {
        base.ExitState();

        Player.InputReader.BasicAttackEvent -= AttackHandle;
        Player.InputReader.DodgeEvent -= DodgeHandle;
    }

    private void AttackHandle()
    {
        if (!Player.PlayerAttackCompo.CanAttack) return;

        _stateMachine.ChangeState(StateTypeEnum.BasicAttack);
    }

    private void DodgeHandle()
    {
        if (Player.PlayerAttackCompo.CanAttack)
            _stateMachine.ChangeState(StateTypeEnum.Dodge);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Player.MoveCompo.Move();
    }
}
