using System;
using MessagePipe;
using UnityEngine;
using VContainer;

namespace Player
{
    public class RotationController : MonoBehaviour
    {
        private float _rotationSpeed = 720f;
        private float _rotationSpeedToEnemy = 30f;
        private int _minDistance = 8;
        private Vector3 _lastDirection;

        private IInputHandler _inputHandler;
        private Enemy _enemy;
        private IDisposable _subscriber;

        [Inject]
        public void Construct(IInputHandler inputHandler, ISubscriber<EnemyIsNearbyMessage> enemyIsNearbySubscriber)
        {
            _inputHandler = inputHandler;
            _subscriber = enemyIsNearbySubscriber.Subscribe(SetEnemy);
        }

        private void SetEnemy(EnemyIsNearbyMessage enemyIsNearbyMessage)
        {
            _enemy = enemyIsNearbyMessage.Enemy;
        }

        private void Update()
        {
            if (_enemy != null)
            {
                var target = _enemy.transform.position;
                var isEnemyNearby  = Vector3.Distance(transform.position, target) < _minDistance;

                if (isEnemyNearby)
                {
                    RotateTowardsEnemy(target);
                }
                else
                {
                    RotateBasedOnInput();
                }
            }
            else
            {
                RotateBasedOnInput();
            }
        }

        private void RotateTowardsEnemy(Vector3 target)
        {
            var direction = target - transform.position;
            direction.y = 0;
            var targetRotation = Quaternion.LookRotation(direction.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeedToEnemy * Time.deltaTime);
        }

        private void RotateBasedOnInput()
        {
            var input = _inputHandler.GetInput();
            var direction = new Vector3(input.x, 0, input.y);

            if (direction != Vector3.zero)
            {
                _lastDirection = direction;
            }

            Quaternion toRotation = Quaternion.LookRotation(_lastDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
        }

        private void OnDestroy()
        {
            _subscriber.Dispose();
        }
    }
}
