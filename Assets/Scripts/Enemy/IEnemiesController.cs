using System.Collections.Generic;

public interface IEnemiesController
{
    public List<Enemy> AliveEnemies { get; }

    public bool HasAliveEnemies();

}