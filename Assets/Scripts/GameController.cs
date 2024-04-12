using VContainer.Unity;

public class GameController : IStartable
{
    private WavesController _wavesController;


    public GameController(WavesController wavesController)
    {
        _wavesController = wavesController;
    }

    public void Start()
    {
      _wavesController.StartSpawn();
    }
}