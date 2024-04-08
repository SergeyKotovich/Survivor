using VContainer.Unity;

public class GameController : IStartable
{
    private EnemiesSpawner _enemiesSpawner;

    public GameController(EnemiesSpawner enemiesSpawner)
    {
        _enemiesSpawner = enemiesSpawner;
    }

    public async void Start()
    {
       await _enemiesSpawner.SpawnEnemies();
    }
}