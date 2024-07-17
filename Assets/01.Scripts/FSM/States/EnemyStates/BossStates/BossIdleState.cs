using UnityEngine;

public class BossIdleState : BossBaseState
{
    public override void EnterState()
    {
        base.EnterState();

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
