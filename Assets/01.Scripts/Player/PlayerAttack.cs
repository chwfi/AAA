using UnityEngine;

public class PlayerAttack : AttackableEntity, IPlayerable
{
    public PlayerController Player { get; set; }

    public int CurrentComboCounter = 0;
    public bool CanAttack = true;

    protected override void Awake()
    {
        base.Awake();
    }

    public void SetPlayer(PlayerController player)
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

        if (Vector3.Distance(transform.position, enemyColl.ClosestPoint(transform.position)) < stopRange) return true;
        else return false;
    }

    public void RotateToTarget()
    {
        Vector3 dir = new Vector3(enemyColl.ClosestPoint(transform.position).x, transform.position.y,
            enemyColl.ClosestPoint(transform.position).z);
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
        DamageCasterCompo.CastDamage(TargetLayer);
    }
}
