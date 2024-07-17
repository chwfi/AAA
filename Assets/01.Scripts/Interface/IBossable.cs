public interface IBossable
{
    public BossController Boss { get; set; }

    public void SetOwner(BossController enemy) { }
}
