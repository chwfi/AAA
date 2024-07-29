using UnityEngine;

public class BossController : Entity
{
    public BossMove MoveCompo { get; private set; }
    public BossAnimator AnimatorCompo { get; private set; }
    public CharacterController CharacterControllerCompo { get; private set; }
    public BossSkillController BossSkillController { get; private set; }
    public StateMachine BossStateMachine { get; private set; }
    public BossAttack BossAttackCompo { get => (BossAttack)AttackCompo; }

    protected override void Awake()
    {
        base.Awake();

        Transform visual = transform.Find("Visual").transform;

        //MoveCompo = GetComponent<BossMove>();
        //CharacterControllerCompo = GetComponent<CharacterController>();
        //BossSkillController = GetComponent<BossSkillController>();
        AnimatorCompo = visual.GetComponent<BossAnimator>();

        //SetEnemyComponents();
        //SetStates();
    }

    private void SetEnemyComponents()
    {
        IBossable[] enemyCompos = transform.GetComponentsInChildren<IBossable>();

        foreach (IBossable enemy in enemyCompos)
        {
            enemy.SetOwner(this);
        }
    }

    private void SetStates()
    {
        BossStateMachine = new StateMachine();

        Transform states = transform.Find("States").transform;
        var stateComponents = states.GetComponents<State>();

        foreach (var stateComponent in stateComponents)
        {
            stateComponent.Initialize(BossStateMachine, this, BossAttackCompo);
            BossStateMachine.AddState(stateComponent.StateType, stateComponent);
        }
    }

    private void Start()
    {
        //BossStateMachine.Init(StateTypeEnum.Idle);
    }

    private void Update()
    {
        //BossStateMachine.CurrentState.UpdateState();
    }
}
