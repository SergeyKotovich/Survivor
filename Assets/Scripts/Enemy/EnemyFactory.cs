using UnityEngine;
using VContainer;
using VContainer.Unity;

public class EnemyFactory
{
    private IObjectResolver _objectResolver;

    public EnemyFactory(IObjectResolver objectResolver)
    {
        _objectResolver = objectResolver;
    }

    public Enemy CreateEnemy(EnemyConfig enemyConfig, Vector3 position)
    {
        var enemy = _objectResolver.Instantiate(enemyConfig.EnemyPrefab, position, Quaternion.identity);
        enemy.Initialize(enemyConfig);
        return enemy;
    }
}