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
        Rotate(input);

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

    private void Rotate(Vector2 input)
    {
        var horizontal = input.x;
        var vertical = input.y;
        var rotationY = 180f;

        if (horizontal != 0)
        {
            rotationY = horizontal > 0 ? 90f : -90f;
        }
        else if (vertical != 0)
        {
            rotationY = vertical > 0 ? 0f : 180f;
        }

        var newRotation = Quaternion.Euler(0f, rotationY, 0f);

        transform.rotation = newRotation;
    }
}