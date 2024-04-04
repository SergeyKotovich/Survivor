public struct EnemyIsNearbyMessage
{
    public readonly bool HasEnemy;

    public EnemyIsNearbyMessage(bool hasEnemy)
    {
        HasEnemy = hasEnemy;
    }
}