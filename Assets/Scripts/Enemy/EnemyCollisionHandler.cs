using MessagePipe;
using UnityEngine;
using VContainer;

public class EnemyCollisionHandler : MonoBehaviour
{
    private IPublisher<EnemyDiedMessage> _enemyDiedPublisher;

    [Inject]
    public void Construct(IPublisher<EnemyDiedMessage> enemyDiedPublisher)
    {
        _enemyDiedPublisher = enemyDiedPublisher;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(GlobalConstants.BULLET_TAG))
        {
           _enemyDiedPublisher.Publish(new EnemyDiedMessage());
        }
    }
}