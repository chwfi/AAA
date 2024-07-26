using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryingState : PlayerGroundedState
{
    public override void EnterState()
    {
        base.EnterState();

        Player.MoveCompo.StopImmediately();
        Player.MoveCompo.CanMove = false;
        Player.AnimatorCompo.SetParryingAnimation(true);
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void ExitState()
    {
        base.ExitState();
        Player.AnimatorCompo.SetParryingAnimation(false);
        Player.MoveCompo.CanMove = true;
    }
}
