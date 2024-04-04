using System;
using MessagePipe;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;

public class EnemyCollisionHandler : MonoBehaviour, ICollision
{
    private IPublisher<EnemyDiedMessage> _enemyDiedPublisher;
    public event Action EnemyDied;
    
    [Inject]
    public void Construct(IPublisher<EnemyDiedMessage> enemyDiedPublisher)
    {
        _enemyDiedPublisher = enemyDiedPublisher;
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        var enemy = GetComponent<Enemy>();
        if (collision.collider.CompareTag(GlobalConstants.BULLET_TAG))
        {
            EnemyDied?.Invoke();
            _enemyDiedPublisher.Publish(new EnemyDiedMessage(enemy));
        }
    }
}