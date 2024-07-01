using UnityEngine;

public abstract class State : MonoBehaviour
{
    public StateTypeEnum StateType;

    protected StateMachine _stateMachine;
    protected PlayerController _owner;
    protected bool _triggerCalled = true;

    public virtual void Initialize(StateMachine stateMachine, PlayerController owner)
    {
        _stateMachine = stateMachine;
        _owner = owner;
    }

    public virtual void EnterState() { }

    public virtual void UpdateState() { }

    public virtual void ExitState() { }
}
