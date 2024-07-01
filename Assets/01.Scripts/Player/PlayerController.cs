using System.Collections;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    public InputReader InputReader => _inputReader;
    public CharacterController CharacterControllerCompo { get; private set; }
    public PlayerAnimator AnimatorCompo { get; private set; }
    public StateMachine PlayerStateMachine { get; private set; }

    [Header("Player")]
    public float MoveSpeed = 2.0f;
    public float SprintSpeed = 5.335f;
    [Range(0.0f, 0.3f)] public float RotationSmoothTime = 0.12f;
    public float SpeedChangeRate = 10.0f;
    public float JumpHeight = 1.2f;
    public float Gravity = -15.0f;
    public float JumpTimeout = 0.50f;
    public float FallTimeout = 0.15f;

    public int CurrentComboCounter = 0;

    [Header("Player Grounded")]
    public bool Grounded = true;
    public float GroundedOffset = -0.14f;
    public float GroundedRadius = 0.28f;
    public LayerMask GroundLayers;

    [Header("Cinemachine")]
    public GameObject CinemachineCameraTarget;
    public float TopClamp = 70.0f;
    public float BottomClamp = -30.0f;
    public float CameraAngleOverride = 0.0f;
    public bool LockCameraPosition = false;

    private GameObject _mainCamera;
    private Coroutine _dashCoroutine;
    private Vector3 targetDirection;

    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _verticalVelocity;

    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private const float _threshold = 0.01f;

    public bool CanAttack = true;
    public bool CanMove = true;
      
    private void Awake()
    {
        PlayerStateMachine = new StateMachine();

        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        Transform visual = transform.Find("Visual").transform;
        Transform states = transform.Find("States").transform;

        CharacterControllerCompo = GetComponent<CharacterController>();
        AnimatorCompo = visual.GetComponent<PlayerAnimator>();

        var stateComponents = states.GetComponents<State>();

        foreach (var stateComponent in stateComponents)
        {
            stateComponent.Initialize(PlayerStateMachine, this);
            PlayerStateMachine.AddState(stateComponent.StateType, stateComponent);
        }
    }

    private void OnEnable()
    {
        if (_inputReader != null)
        {
            var playerInput = new Controls();
            playerInput.Player.SetCallbacks(_inputReader);
            playerInput.Player.Enable();
        }
    }

    private void Start()
    {
        PlayerStateMachine.Init(StateTypeEnum.Idle);
    }

    private void Update()
    {
        PlayerStateMachine.CurrentState.UpdateState();

        SetGravity();
        GroundCheck();
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        if (_inputReader.Look.sqrMagnitude >= _threshold && !LockCameraPosition)
        {
            float deltaTimeMultiplier = 1.0f;

            _cinemachineTargetYaw += _inputReader.Look.x * deltaTimeMultiplier;
            _cinemachineTargetPitch += _inputReader.Look.y * deltaTimeMultiplier;
        }

        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    public void Move()
    {
        if (!CanMove) return;

        Vector3 inputDirection = new Vector3(_inputReader.MoveInput.x, 0.0f, _inputReader.MoveInput.y).normalized;

        AnimatorCompo.SetMoveAnimation(MoveSpeed, SpeedChangeRate, inputDirection.magnitude);

        if (_inputReader.MoveInput != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                RotationSmoothTime);

            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        CharacterControllerCompo.Move(targetDirection.normalized * (MoveSpeed * Time.deltaTime) +
                         new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }

    public void Dash(Vector3 dir, float delay, float time, float speed)
    {
        if (_dashCoroutine != null)
            StopCoroutine(_dashCoroutine);

        _dashCoroutine = StartCoroutine(DashCoroutine(dir, delay, time, speed));
    }

    private IEnumerator DashCoroutine(Vector3 dir, float delay, float time, float speed)
    {
        yield return new WaitForSeconds(delay);

        float startTime = Time.time;

        while (Time.time < startTime + time)
        {
            CharacterControllerCompo.Move
                (dir * (speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

            yield return null;
        }
    }

    public void StopImmediately()
    {
        CharacterControllerCompo.Move(Vector3.zero);
    }

    public void GroundCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
            transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
            QueryTriggerInteraction.Ignore);

        AnimatorCompo.SetGroundedAnimation(Grounded);
    }

    public void SetGravity()
    {
        _verticalVelocity += Time.deltaTime * Gravity;
    }

    public void InitAttackCombo()
    {
        CanMove = true;
        CanAttack = true;
        CurrentComboCounter = 0;
        AnimatorCompo.SetAttackCount(CurrentComboCounter);
        AnimatorCompo.SetAttackAnimation(false);
    }
}
