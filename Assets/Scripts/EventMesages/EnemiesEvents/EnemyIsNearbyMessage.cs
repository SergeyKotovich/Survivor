public struct EnemyIsNearbyMessage
{
    public readonly Enemy Enemy;

    public EnemyIsNearbyMessage(Enemy enemy)
    {
        Enemy = enemy;
    }
}