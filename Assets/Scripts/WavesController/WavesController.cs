using UnityEngine;
using VContainer;

public class WavesController : MonoBehaviour
{
   private EnemiesSpawner _enemiesSpawner;

   [Inject]
   public void Construct(EnemiesSpawner enemiesSpawner)
   {
      _enemiesSpawner = enemiesSpawner;
   }
}
