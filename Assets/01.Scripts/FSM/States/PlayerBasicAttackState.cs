using UnityEngine;

public class PlayerBasicAttackState : PlayerGroundedState
{
    [SerializeField] private float _dashDelay;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _targetDashTime;
    [SerializeField] private float _targetDashSpeed;

    public override void EnterState()
    {
        base.EnterState();

        Player.MoveCompo.StopImmediately();
        Player.MoveCompo.CanMove = false;
        Player.AttackCompo.AttackFeedback.ApplyAttackEffect();
        Player.AttackCompo.AttackFeedback.ApplyAttackSound();

        if (_attack.CurrentTarget != null) //타겟이 있다면 더 빠른 대쉬
        {
            PlayerAttack.RotateToTarget();
            Player.MoveCompo.Dash(Player.AnimatorCompo.transform.forward, 
                _dashDelay, _targetDashTime, _targetDashSpeed, DashTypeEnum.AttackDash);
        }
        else
        {
            Player.MoveCompo.Dash(Player.AnimatorCompo.transform.forward, 
                _dashDelay, _dashTime, _dashSpeed, DashTypeEnum.AttackDash);
        }

        Player.AnimatorCompo.SetAttackCount(Player.AttackCompo.CurrentComboCounter);
        Player.AnimatorCompo.SetAttackAnimation(true);
        Player.AttackCompo.CurrentComboCounter++;
        Player.AttackCompo.CanAttack = false;

        if (Player.AttackCompo.CurrentComboCounter == 2)
            _dashDelay = 0.25f;
        else
            _dashDelay = 0.15f;
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