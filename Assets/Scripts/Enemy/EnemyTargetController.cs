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

    private const float _minDistance = 1.5f;
    private bool _wasTargetNearby;
    private bool _hasTarget;

    private IMovable _target;

    public void Initialize(float speed)
    {
        _navMeshAgent.speed = speed;
    }

    private void Update()
    {
        if (_target != null)
        {
            if (_hasTarget)
            {
                transform.LookAt(_target.Position);
                _navMeshAgent.destination = _target.Position;
            }

            var isTargetNearby = IsTargetNearby();
            var isTargetNearbyChanged = _wasTargetNearby != isTargetNearby;
            if (!isTargetNearbyChanged)
            {
                return;
            }

            if (isTargetNearby)
            {
                TargetNearby?.Invoke();
            }
            else
            {
                TargetFar?.Invoke();
            }

            _wasTargetNearby = isTargetNearby;
        }
    }

    public void SetTarget(IMovable target)
    {
        _target = target;
        _navMeshAgent.isStopped = false;
        _hasTarget = true;
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
        }
    }

    public void Stop()
    {
        _target = null;
        _navMeshAgent.isStopped = true;
    }
}