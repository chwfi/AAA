using UnityEngine;

public class PlayerAttack : AttackableEntity, IPlayerable
{
    public AttackFeedback AttackFeedback { get; private set; }
    public PlayerController Player { get; set; }

    public int CurrentComboCounter = 0;
    public bool CanAttack = true;

    protected override void Awake()
    {
        base.Awake();

        Transform feedbackTrm = transform.Find("Feedback").transform;
        AttackFeedback = feedbackTrm.GetComponent<AttackFeedback>();
        AttackFeedback.SetOwner(transform.GetComponent<Entity>());
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

        if (Vector3.Distance(transform.position, CurrentTarget.transform.position) < stopRange) return true;
        else return false;
    }

    public void RotateToTarget()
    {
        var destRotation = Quaternion.LookRotation(CurrentTarget.transform.position - transform.position);
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
