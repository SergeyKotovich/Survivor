using System;
using MessagePipe;
using UnityEngine;
using VContainer;

public class PlayerMovementController : MonoBehaviour, ISpeedImprovable
{
    public event Action<float, float> RunningSpeedUpdated;
    
    [SerializeField] private PlayerAnimationController _animationController;
    [SerializeField] private CharacterController _characterController;
    
    private float _speed;
    
    private IInputHandler _inputHandler;

    private bool isPlayerAlive = true;

    private IDisposable _subscriber;
    private PlayerConfig _playerConfig;

    [Inject]
    public void Construct(IInputHandler inputHandler, ISubscriber<PlayerDiedMessage> playerDiedSubscriber )
    {
        _inputHandler = inputHandler;
        _subscriber = playerDiedSubscriber.Subscribe(_ => isPlayerAlive = false);
    }

    private void Start()
    {
        RunningSpeedUpdated?.Invoke(_speed, _speed + _playerConfig.RunningSpeedImprovementStep);
    }

    public void Initialize(PlayerConfig playerConfig)
    {
        _playerConfig = playerConfig;
        _speed = _playerConfig.RunningSpeed;
    }

    private void Update()
    {
        if (isPlayerAlive)
        {
            Move(); 
        }
    }

    public void ImproveRunningSpeed()
    {
        _speed += _playerConfig.RunningSpeedImprovementStep;
        RunningSpeedUpdated?.Invoke(_speed, _speed + _playerConfig.RunningSpeedImprovementStep);
    }

    private void Move()
    {
        var input = _inputHandler.GetInput();
        
        var direction = new Vector3(input.x, 0, input.y).normalized;
        if (direction == Vector3.zero)
        {
            _animationController.PlayIdleAnimation();
            _animationController.StopRunningAnimation();
        }
        else
        {
            _characterController.Move(direction * _speed * Time.deltaTime);
            _animationController.StopIdleAnimation();
            _animationController.PlayRunningAnimation();
        }
    }

    private void OnDestroy()
    {
        _subscriber.Dispose();
    }
}