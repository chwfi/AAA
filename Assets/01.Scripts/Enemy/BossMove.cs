using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour, IBossable
{
    [Header("Value")]
    public float MoveSpeed = 2.0f;
    public float SprintSpeed = 5.335f;
    [Range(0.0f, 0.3f)] public float RotationSmoothTime = 0.12f;
    public float SpeedChangeRate = 10.0f;
    public float JumpHeight = 1.2f;
    public float Gravity = -15.0f;
    public float JumpTimeout = 0.50f;
    public float FallTimeout = 0.15f;

    private Coroutine _dashCoroutine;
    private Vector3 targetDirection;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _verticalVelocity;
    public float VerticalVelocity => _verticalVelocity;

    public bool CanMove = true;

    public CharacterController CharacterControllerCompo { get; private set; }
    public BossController Boss { get; set; }

    private void Awake()
    {
        CharacterControllerCompo = GetComponent<CharacterController>();
    }

    public void SetOwner(BossController boss)
    {
        Boss = boss;
    }

    public void Update()
    {
        SetGravity();
        
    }

    public void SetGravity()
    {
        _verticalVelocity += Gravity * Time.deltaTime;

        Vector3 moveVector = new Vector3(0.0f, _verticalVelocity, 0.0f);
        CharacterControllerCompo.Move(moveVector * Time.deltaTime);
    }

    public void Move()
    {
        if (!CanMove) return;                      

        Boss.AnimatorCompo.SetMoveAnimation(MoveSpeed, SpeedChangeRate);

        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
            RotationSmoothTime);

        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

        targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        CharacterControllerCompo.Move(targetDirection.normalized * (MoveSpeed * Time.deltaTime) +
                         new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }

    public void Dash(Vector3 dir, float delay, float time, float speed, DashTypeEnum dashType, bool ease = false)
    {
        if (_dashCoroutine != null)
            StopCoroutine(_dashCoroutine);

        _dashCoroutine = StartCoroutine(DashCoroutine(dir, delay, time, speed, dashType, ease));
    }

    public void JumpDash(Vector3 dir, float delay, float time, float speed, float jumpForce, float multfyForce)
    {
        if (_dashCoroutine != null)
            StopCoroutine(_dashCoroutine);

        _dashCoroutine = StartCoroutine(JumpDashCoroutine(dir, delay, time, speed, jumpForce, multfyForce));
        Debug.Log("스타트 대쉬");
    }

    private IEnumerator JumpDashCoroutine(Vector3 dir, float delay, float time, float speed, float jumpForce, float multfyForce)
    {
        yield return new WaitForSeconds(delay);

        //점프 초기 속도
        _verticalVelocity = jumpForce;

        float startTime = Time.time;

        while (Time.time < startTime + time)
        {
            float elapsed = Time.time - startTime;
            float currentSpeed = speed * OutQuint(elapsed / time);

            _verticalVelocity += Gravity * Time.deltaTime * multfyForce;

            Vector3 move = dir * (currentSpeed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime;

            CharacterControllerCompo.Move(move);

            yield return null;
        }

        _verticalVelocity = 0.0f;
    }


    private IEnumerator DashCoroutine(Vector3 dir, float delay, float time, float speed, DashTypeEnum dashType, bool ease = false)
    {
        yield return new WaitForSeconds(delay);

        float startTime = Time.time;

        while (Time.time < startTime + time)
        {
            float elapsed = Time.time - startTime;
            float currentSpeed = ease ? speed * OutQuint(elapsed / time) : speed;

            //if (Boss.BossAttackCompo.IsTargetInStopRange() && dashType == DashTypeEnum.AttackDash)
            //{
            //    currentSpeed = 0;
            //    StopCoroutine(_dashCoroutine);
            //}

            CharacterControllerCompo.Move
                (dir * (currentSpeed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

            yield return null;
        }
    }

    public void StopImmediately()
    {
        CharacterControllerCompo.Move(Vector3.zero);
    }

    private float OutQuint(float t)
    {
        return 1f - Mathf.Pow(1f - t, 5f);
    }
}
