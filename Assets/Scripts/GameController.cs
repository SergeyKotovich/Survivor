using VContainer.Unity;

public class GameController : IStartable
{
    private EnemySpawner _enemySpawner;

    public GameController(EnemySpawner enemySpawner)
    {
        _enemySpawner = enemySpawner;
    }

    public async void Start()
    {
       await _enemySpawner.SpawnEnemies();
    }
}