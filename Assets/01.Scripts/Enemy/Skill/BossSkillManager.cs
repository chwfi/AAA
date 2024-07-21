using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossSkillTypeEnum
{
    MeleeAttack,
    JumpAttack,
    MissileAttack,
}

public class BossSkillManager
{
    public List<BossSkill> SkillList = new();

    public void SetSkills()
    {
        foreach (var skill in SkillList)
        {
            skill.SetCoolDown();
        }
    }
     
    public void Update()
    {
        foreach (var skill in SkillList)
        {
            if (skill.CanExecute())
            {
                skill.Execute();
            }
        }
    }
}

