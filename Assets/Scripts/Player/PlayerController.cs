using UnityEngine;
using VContainer;

public class PlayerController : MonoBehaviour, IMovable
{
    public Vector3 Position => transform.position;
    
    [SerializeField] private MovementController _movementController;
    
}