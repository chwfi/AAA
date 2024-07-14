using UnityEngine;

public class PlayerAttack : TargetController, IPlayerable
{
    public BoxCollider coll;
    public PlayerController Player { get; set; }

    public int CurrentComboCounter = 0;
    public bool CanAttack = true;

    protected override void Awake()
    {
        base.Awake();
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

    public void RotateToTarget()
    {
        if (CurrentTarget == null) return;

        Vector3 dir = new Vector3(CurrentTarget.ClosestPoint(transform.position).x, transform.position.y,
            CurrentTarget.ClosestPoint(transform.position).z);
        var destRotation = Quaternion.LookRotation(dir - transform.position);
        transform.rotation = destRotation;
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
        coll.enabled = true;
    }

    public void AttackEndTrigger()
    {
        coll.enabled = false;
    }
}
