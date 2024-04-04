using UnityEngine;
using VContainer;

public class PlayerController : MonoBehaviour, IMovable, ITarget
{
    public Vector3 Position => transform.position;
    
    [SerializeField] private MovementController _movementController;
    [SerializeField] private PlayerTargetController _playerTargetController;

    public Enemy GetTarget()
    {
       return _playerTargetController.NearestEnemy;
    }

    public bool HasTarget()
    {
        return _playerTargetController.HasTarget();
    }

}