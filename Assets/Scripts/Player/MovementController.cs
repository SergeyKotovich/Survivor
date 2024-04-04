using System;
using MessagePipe;
using UnityEngine;
using VContainer;

public class MovementController : MonoBehaviour
{
    [SerializeField] private PlayerAnimationController _animationController;
    [SerializeField] private CharacterController _characterController;

    [SerializeField] private float _speed;
    
    private IInputHandler _inputHandler;

    [Inject]
    public void Construct(IInputHandler inputHandler)
    {
        _inputHandler = inputHandler;
        
    }

    private void Update()
    {
        Move();
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
}