using UnityEngine;

public abstract class State : MonoBehaviour
{
    public StateTypeEnum StateType;

    protected StateMachine _stateMachine;
    protected Entity _owner;
    protected TargetController _attack;

    public virtual void Initialize(StateMachine stateMachine, Entity owner, TargetController attackController)
    {
        _stateMachine = stateMachine;
        _owner = owner;
        _attack = attackController;
    }

    public virtual void EnterState() { }

    public virtual void UpdateState() { }

    public virtual void ExitState() { }
}
