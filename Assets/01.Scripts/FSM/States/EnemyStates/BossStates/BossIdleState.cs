using UnityEngine;

public class BossIdleState : BossGroundedState
{
    public override void EnterState()
    {
        base.EnterState();

        Boss.AnimatorCompo.InitAnimation();
        Boss.MoveCompo.MoveSpeed = 0;
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
