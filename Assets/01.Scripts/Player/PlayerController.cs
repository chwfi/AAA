using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class PlayerController : Entity
{
    [SerializeField] private InputReader _inputReader;
    public InputReader InputReader => _inputReader;

    public PlayerMove MoveCompo { get; private set; }
    public PlayerAttack AttackCompo { get; private set; }
    public PlayerAnimator AnimatorCompo { get; private set; }
    public StateMachine PlayerStateMachine { get; private set; }

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

    public GameObject MainCamera { get; private set; }
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private const float _threshold = 0.01f;
      
    protected override void Awake()
    {
        base.Awake();

        Transform visual = transform.Find("Visual").transform;

        MoveCompo = GetComponent<PlayerMove>();
        AttackCompo = GetComponent<PlayerAttack>();
        AnimatorCompo = visual.GetComponent<PlayerAnimator>();

        SetPlayerComponents();
        SetStates();
    }

    private void SetPlayerComponents()
    {
        IPlayerable[] playerCompos = transform.GetComponentsInChildren<IPlayerable>();

        foreach (IPlayerable player in playerCompos)
        {
            player.SetPlayer(this);
        }
    }

    private void SetStates()
    {
        PlayerStateMachine = new StateMachine();

        Transform states = transform.Find("States").transform;
        var stateComponents = states.GetComponents<State>();

        foreach (var stateComponent in stateComponents)
        {
            stateComponent.Initialize(PlayerStateMachine, this, AttackCompo);
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
        MainCamera = GameManager.Instance.MainCam;

        PlayerStateMachine.Init(StateTypeEnum.Idle);
    }

    private void Update()
    {
        PlayerStateMachine.CurrentState.UpdateState();

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

    public void GroundCheck()
    {
        Vector3 spherePosition = new(transform.position.x, transform.position.y - GroundedOffset,
            transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
            QueryTriggerInteraction.Ignore);

        AnimatorCompo.SetGroundedAnimation(Grounded);
    }
}
