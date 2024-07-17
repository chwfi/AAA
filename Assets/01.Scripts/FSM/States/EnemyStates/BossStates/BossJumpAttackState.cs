using UnityEngine;

public class BossJumpAttackState : BossBaseState
{
    [SerializeField] private float _dashDelay;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _multiplyForce;
    [SerializeField] private float _coolDown;

    public override void EnterState()
    {
        base.EnterState();

        Boss.BossAttackCompo.RotateToTarget();
        Boss.MoveCompo.JumpDash
            (Boss.AnimatorCompo.transform.forward,
            _dashDelay, _dashTime, _dashSpeed, _jumpForce, _multiplyForce);
        Boss.AnimatorCompo.SetJumpAttackAnimation(true);
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void ExitState()
    {
        Boss.BossAttackCompo.RotateToTarget();
        base.ExitState();
        Boss.SkillPatternCompo.SetCoolDownHandler(_coolDown, BossAttackType.JumpAttack);
    }
}
