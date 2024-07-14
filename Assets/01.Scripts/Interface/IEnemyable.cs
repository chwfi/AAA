public interface IEnemyable
{
    public EnemyController Enemy { get; set; }

    public void SetOwner(EnemyController enemy) { }
}
