using System;
using MessagePipe;
using UnityEngine;
using VContainer;

namespace Player
{
    public class RotationController : MonoBehaviour
    {
        private bool _hasEnemyNearby;

        private IInputHandler _inputHandler;
        private IDisposable _subscriber;

        [Inject]
        public void Construct(IInputHandler inputHandler, ISubscriber<EnemyIsNearbyMessage> enemyIsNearbySubscriber)
        {
            _inputHandler = inputHandler;
            _subscriber = enemyIsNearbySubscriber.Subscribe(ChangeFlag);
        }

        private void Update()
        {
            if (_hasEnemyNearby)
            {
                return;
            }

            Rotate();
        }

        private void ChangeFlag(EnemyIsNearbyMessage enemyIsNearbyMessage)
        {
            _hasEnemyNearby = enemyIsNearbyMessage.HasEnemy;
        }

        private void Rotate()
        {
            var input = _inputHandler.GetInput();


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

        private void OnDestroy()
        {
            _subscriber.Dispose();
        }
    }
}