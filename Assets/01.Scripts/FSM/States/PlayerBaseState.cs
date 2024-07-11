using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : State
{
    protected PlayerController Player => _owner as PlayerController;
    protected PlayerAttack PlayerAttack => _attack as PlayerAttack;
}
