using UnityEngine;

public class BossBaseState : State
{
    protected BossController Boss => _owner as BossController;
}