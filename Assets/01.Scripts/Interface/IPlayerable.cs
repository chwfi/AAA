public interface IPlayerable
{
    public PlayerController Player { get; set; }

    public void SetOwner(PlayerController player) { }
}
