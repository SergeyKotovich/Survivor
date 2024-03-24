using System;
using UnityEngine;
using VContainer;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private CharacterController _characterController;
    private IInputHandler _inputHandler;

    [Inject]
    public void Construct(IInputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }

    private void Update()
    {
        var input = _inputHandler.GetInput();
        var direction = new Vector3(input.x, 0, input.y).normalized;
        _characterController.Move(direction * _speed * Time.deltaTime);
    }
}