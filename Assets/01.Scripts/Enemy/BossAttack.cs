using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : TargetController, IBossable
{
    public BossController Boss { get; set; }

    public void SetOwner(BossController enemy)
    {
        Boss = enemy;
    }
}
