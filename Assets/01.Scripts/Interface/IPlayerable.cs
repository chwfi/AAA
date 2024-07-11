public interface IPlayerable
{
    public PlayerController Player { get; set; }

    public void SetPlayer(PlayerController player) { }
}
