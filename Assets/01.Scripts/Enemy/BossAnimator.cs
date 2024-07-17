using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimator : MonoBehaviour, IBossable
{
    private Animator _animator;
    public Animator AnimatorCompo => _animator;

    private readonly int _speedAnim = Animator.StringToHash("Speed");
    private readonly int _motionSpeedAnim = Animator.StringToHash("MotionSpeed");
    private readonly int _attackAnim = Animator.StringToHash("Attack");
    private readonly int _attackTrigger = Animator.StringToHash("AttackTrigger");
    private readonly int _jumpAttackAnim = Animator.StringToHash("JumpAttack");
    private float _animationBlend;

    public BossController Boss { get; set; }

    public void SetOwner(BossController boss)
    {
        Boss = boss;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetRootMotion(bool value)
    {
        _animator.applyRootMotion = value;
    }

    public void SetMoveAnimation(float targetSpeed, float SpeedChangeRate)
    {
        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
        if (_animationBlend < 0.01f) _animationBlend = 0f;

        _animator.SetFloat(_speedAnim, _animationBlend);
        _animator.SetFloat(_motionSpeedAnim, 1);
    }

    public void SetJumpAttackAnimation(bool value)
    {
        _animator.SetBool(_jumpAttackAnim, value);
    }

    public void SetAttackTrigger()
    {
        _animator.SetTrigger(_attackTrigger);
    }

    public void SetBasicAttackAnimation(bool value)
    {
        _animator.SetBool(_attackAnim, value);
    }

    public void BasicAttackEndTrigger()
    {
        SetBasicAttackAnimation(false);
        Boss.BossStateMachine.ChangeState(StateTypeEnum.Idle);
    }

    public void JumpAttackEndTrigger()
    {
        SetJumpAttackAnimation(false);
        Boss.BossStateMachine.ChangeState(StateTypeEnum.Idle);
    }

    public void RestartAttackTrigger()
    {
        Boss.BossStateMachine.ChangeState(StateTypeEnum.BasicAttack);
    }
}