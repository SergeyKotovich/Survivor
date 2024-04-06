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
    
    public void Initialize(float speed)
    {
        _navMeshAgent.speed = speed;
    }

    private void Update()
    {
        if (_target!=null)
        {
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
        _navMeshAgent.isStopped = true;
    }

   
}