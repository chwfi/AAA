using UnityEngine;

public class PlayerAttack : TargetController, IPlayerable
{
    public TriggerCaster TriggerCompo { get; private set; }
    public PlayerController Player { get; set; }

    public int CurrentComboCounter = 0;
    public bool CanAttack = true;

    protected override void Awake()
    {
        base.Awake();

        TriggerCompo = GetComponentInChildren<TriggerCaster>();
    }

    public void SetOwner(PlayerController player)
    {
        Player = player;
    }

    protected override void Update()
    {
        base.Update();
    }

    public bool IsTargetInStopRange()
    {
        if (CurrentTarget == null) return false;

        if (CurrentTarget != null && Vector3.Distance(transform.position, CurrentTarget.ClosestPoint(transform.position)) < stopRange) return true;
        else return false;
    }

    public void InitAttackCombo()
    {
        Player.MoveCompo.CanMove = true;
        CanAttack = true;
        CurrentComboCounter = 0;
        Player.AnimatorCompo.SetAttackCount(CurrentComboCounter);
        Player.AnimatorCompo.SetAttackAnimation(false);
    }

    public override void AttackTrigger()
    {
        TriggerCompo.SetTrigger(true);
    }

    public void AttackEndTrigger()
    {
        TriggerCompo.SetTrigger(false);
    }
}
