using System;
using MessagePipe;
using VContainer.Unity;

public class GameController : IStartable, IDisposable
{
    private readonly WavesController _wavesController;
    private readonly UIController _uiController;
    private readonly IDisposable _subscriber;


    public GameController(WavesController wavesController, 
        UIController uiController, 
        ISubscriber<PlayerDiedMessage> playerDiedSubscriber)
    {
        _uiController = uiController;
        _wavesController = wavesController;
        _subscriber = playerDiedSubscriber.Subscribe(_ => EndGame());
    }

    public void Start()
    {
        _wavesController.StartSpawn();
        SoundsManager.Instance.PlayMusicGame();
    }

    private void EndGame()
    {
        _uiController.ShowDeathScreen();
        SoundsManager.Instance.PlayEndGame();
    }

    public void Dispose()
    {
        _subscriber.Dispose();
    }
}