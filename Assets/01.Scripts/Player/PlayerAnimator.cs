using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private PlayerController _player;

    private readonly int _speedAnim = Animator.StringToHash("Speed");
    private readonly int _motionSpeedAnim = Animator.StringToHash("MotionSpeed");
    private readonly int _groundedAnim = Animator.StringToHash("Grounded");
    private readonly int _attackAnim = Animator.StringToHash("Attack");
    private readonly int _attackCount = Animator.StringToHash("Attack_Count");
    private readonly int _dodgeAnim = Animator.StringToHash("Dodge");
    private readonly int _dodgeLeftAnim = Animator.StringToHash("Dodge_L");
    private readonly int _dodgeRightAnim = Animator.StringToHash("Dodge_R");
    private readonly int _dodgeBackAnim = Animator.StringToHash("Dodge_B");
    private readonly int _freeFallAnim = Animator.StringToHash("FreeFall");

    private float _animationBlend;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = transform.parent.GetComponent<PlayerController>();
    }

    public void SetMoveAnimation(float targetSpeed, float SpeedChangeRate, float inputMagnitude)
    {
        if (inputMagnitude <= 0f)
        {
            _animationBlend = Mathf.Lerp(_animationBlend, 0, Time.deltaTime * SpeedChangeRate);
        }

        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
        if (_animationBlend < 0.01f) _animationBlend = 0f;

        _animator.SetFloat(_speedAnim, _animationBlend);
        _animator.SetFloat(_motionSpeedAnim, 1);
    }

    public void SetGroundedAnimation(bool value)
    {
        _animator.SetBool(_groundedAnim, value);
    }

    public void SetFreeFallAnimation(bool value)
    {
        _animator.SetBool(_freeFallAnim, value);
    }

    public void SetAttackAnimation(bool value)
    {
        _animator.SetBool(_attackAnim, value);
    }

    public void SetAttackCount(int value)
    {
        _animator.SetInteger(_attackCount, value);
    }

    public void SetAttackTrigger()
    {
        _player.AttackControllerCompo.AttackTrigger();
    }

    public void SetDodgeAnimation(bool value)
    {
        _animator.SetBool(_dodgeAnim, value);
    }

    public void SetDodgeLeftAnimation(bool value)
    {
        _animator.SetBool(_dodgeLeftAnim, value);
    }

    public void SetDodgeRightAnimation(bool value)
    {
        _animator.SetBool(_dodgeRightAnim, value);
    }

    public void SetDodgeBackAnimation(bool value)
    {
        _animator.SetBool(_dodgeBackAnim, value);
    }

    public void DodgeEndTrigger()
    {
        _animator.SetBool(_dodgeAnim, false);
        _animator.SetBool(_dodgeLeftAnim, false);
        _animator.SetBool(_dodgeRightAnim, false);
        _animator.SetBool(_dodgeBackAnim, false);
        _player.PlayerStateMachine.ChangeState(StateTypeEnum.Idle);
    }

    public void AttackEndTrigger()
    {
        _animator.SetBool(_attackAnim, false);
        _player.PlayerStateMachine.ChangeState(StateTypeEnum.Idle);
    }

    public void AttackComboTrigger()
    {
        _player.CanAttack = true;
    }
}
