using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IPlayerable
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

    public bool CanMove = true;

    public CharacterController CharacterControllerCompo { get; private set; }

    public PlayerController Player { get; set; }

    public void SetPlayer(PlayerController player)
    {
        Player = player;
    }

    private void Awake()
    {
        CharacterControllerCompo = GetComponent<CharacterController>();
    }

    public void Update()
    {
        SetGravity();
    }

    public void SetGravity()
    {
        _verticalVelocity += Time.deltaTime * Gravity;
    }

    public void Move()
    {
        if (!CanMove) return;

        Vector3 inputDirection = new Vector3(Player.InputReader.MoveInput.x, 0.0f, Player.InputReader.MoveInput.y).normalized;

        Player.AnimatorCompo.SetMoveAnimation(MoveSpeed, SpeedChangeRate, inputDirection.magnitude);

        if (Player.InputReader.MoveInput != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              Player.MainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                RotationSmoothTime);

            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

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

    private IEnumerator DashCoroutine(Vector3 dir, float delay, float time, float speed, DashTypeEnum dashType, bool ease = false)
    {
        yield return new WaitForSeconds(delay);

        float startTime = Time.time;

        while (Time.time < startTime + time)
        {
            if (Player.AttackCompo.IsTargetInStopRange() && dashType == DashTypeEnum.AttackDash)
                StopCoroutine(_dashCoroutine);

            float elapsed = Time.time - startTime;
            float currentSpeed = ease ? speed * OutQuint(elapsed / time) : speed;

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
