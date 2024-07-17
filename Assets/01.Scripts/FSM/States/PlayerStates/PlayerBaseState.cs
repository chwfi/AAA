public class PlayerBaseState : State
{
    protected PlayerController Player => _owner as PlayerController;
    protected PlayerAttack PlayerAttack => _attack as PlayerAttack;
}
