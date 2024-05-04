using System;
using MessagePipe;
using VContainer.Unity;

public class GameController : IStartable, IDisposable
{
    private WavesController _wavesController;
    private UIController _uiController;
    private IDisposable _subscriber;


    public GameController(WavesController wavesController, UIController uiController, ISubscriber<PlayerDiedMessage> playerDiedSubscriber)
    {
        _uiController = uiController;
        _wavesController = wavesController;
        _subscriber = playerDiedSubscriber.Subscribe(_ => EndGame());
    }

    public void Start()
    {
        _wavesController.StartSpawn();
    }

    private void EndGame()
    {
        _uiController.ShowDeathScreen();
    }

    public void Dispose()
    {
        _subscriber.Dispose();
    }
}