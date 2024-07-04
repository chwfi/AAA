using UnityEngine;

public class PlayerBasicAttackState : PlayerGroundedState
{
    [SerializeField] private float _dashDelay;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _targetDashTime;
    [SerializeField] private float _targetDashSpeed;
    [SerializeField] private float _attackTimer;
    [SerializeField] private float _attackDelay = 0.75f;

    public override void EnterState()
    {
        base.EnterState();

        _owner.StopImmediately();
        _owner.CanMove = false;

        if (_attackController.CurrentTarget != null) //타겟이 있다면 더 빠른 대쉬
        {
            _attackController.AttackTarget();
            _owner.Dash(_owner.AnimatorCompo.transform.forward, 
                _dashDelay, _targetDashTime, _targetDashSpeed, DashTypeEnum.AttackDash);
        }
        else
        {
            _owner.Dash(_owner.AnimatorCompo.transform.forward, 
                _dashDelay, _dashTime, _dashSpeed, DashTypeEnum.AttackDash);
        }

        _owner.AnimatorCompo.SetAttackCount(_owner.CurrentComboCounter);
        _owner.AnimatorCompo.SetAttackAnimation(true);
        _owner.CurrentComboCounter++;
        _owner.CanAttack = false;

        if (_owner.CurrentComboCounter == 2)
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