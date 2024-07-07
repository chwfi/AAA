using UnityEngine;

public abstract class State : MonoBehaviour
{
    public StateTypeEnum StateType;

    protected StateMachine _stateMachine;
    protected PlayerController _owner;
    protected PlayerAttackController _attackController;

    public virtual void Initialize(StateMachine stateMachine, PlayerController owner, PlayerAttackController attackController)
    {
        _stateMachine = stateMachine;
        _owner = owner;
        _attackController = attackController;
    }

    public virtual void EnterState() { }

    public virtual void UpdateState() { }

    public virtual void ExitState() { }
}
