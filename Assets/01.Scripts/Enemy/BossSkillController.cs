using System.Collections;
using UnityEngine;

public class BossSkillController : MonoBehaviour, IBossable
{
    [Header("Condition Value")]
    [SerializeField] private float _meleeAttackDistance;
    [SerializeField] private float _jumpAttackDistance;
    [SerializeField] private float _missileAttackDistance;

    [Header("CoolTime")]
    [SerializeField] private float _meleeAttackCooldown;
    [SerializeField] private float _jumpAttackCooldown;
    [SerializeField] private float _missileAttackCooldown;

    public BossSkillManager SkillManager { get; private set; }
    public BossController Boss { get; set; }

    public bool IsPlayingSkill = false;

    public void SetOwner(BossController boss)
    {
        Boss = boss;

        SkillManager = new BossSkillManager();
        SkillManager.SkillList.Add(new BasicAttackSkill(Boss, transform, this, _meleeAttackDistance, _meleeAttackCooldown));
        SkillManager.SkillList.Add(new JumpAttackSkill(Boss, transform, this, _jumpAttackDistance, _jumpAttackCooldown));
        SkillManager.SkillList.Add(new MissileAttackSkill(Boss, transform, this, _missileAttackDistance, _missileAttackCooldown));

        SkillManager.SetSkills();
    }

    private void Update()
    {
        if (Boss.AttackCompo.CurrentTarget == null) return;

        SkillManager.Update();
    }
}
