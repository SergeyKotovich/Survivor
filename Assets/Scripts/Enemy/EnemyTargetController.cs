using System;
using MessagePipe;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

public class EnemyTargetController : MonoBehaviour
{
    public event Action TargetNearby;
    public event Action TargetFar;
    
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private float _minDistance = 1.5f;
    
    private IMovable _target;
    private Rigidbody _rigidbody;
    
    public void Initialize(float speed)
    {
        _navMeshAgent.speed = speed;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_target!=null)
        {
            transform.LookAt(_target.Position);
            if (IsTargetNearby())
            {
                TargetNearby?.Invoke();
                return;
            }

            if (!IsTargetNearby())
            {
                TargetFar?.Invoke();
            }
        }
    }
    
    public void SetTarget(IMovable target)
    {
        _target = target;
        _navMeshAgent.destination = _target.Position;
    }

    private bool IsTargetNearby()
    {
        return Vector3.Distance(transform.position, _target.Position) < _minDistance;
    }

    public void ResetTarget()
    {
        if (_navMeshAgent.isActiveAndEnabled)
        {
            _navMeshAgent.isStopped = true;
            _rigidbody.isKinematic = true;
        }
    }

   
}