using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGroundedState : BossBaseState
{
    public override void EnterState()
    {
        base.EnterState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Boss.BossAttackCompo.RotateToPlayer();
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
