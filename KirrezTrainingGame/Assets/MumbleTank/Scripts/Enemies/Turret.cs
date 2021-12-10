using UnityEngine;

namespace TankGame
{
    public class Turret : BaseEnemy
    {
        private States _currentState;

        private enum States
        {
            Idle,
            Preparing,
            Activated,
            Deactivated
        }

        public Transform FirePoint;

        private float _preparingTime = 0f;
        private float _maxPreparingTime = 2.5f;

        protected override void OnEnable()
        {
            base.OnEnable();
            _currentState = States.Idle;
        }

        private void FixedUpdate()
        {
            if (_currentState == States.Idle && ActivationRangeReached(1f))
            {
                _currentState = States.Preparing;
                _preparingTime = _maxPreparingTime;
                _startPosition = transform.position;
            }

            if (_currentState == States.Preparing)
            {
                if (_preparingTime >= 0)
                {
                    _preparingTime -= Time.fixedDeltaTime;
                    var finishPosition = transform.position;
                    finishPosition.y = 6f;
                    transform.position = Vector3.Lerp(_startPosition, finishPosition, (1 - _preparingTime) / _maxPreparingTime);
                }
                else
                {
                    _currentState = States.Activated;
                }
            }

            if (_currentState == States.Deactivated && ActivationRangeReached(1f))
            {
                _currentState = States.Activated;
            }

            if (_currentState == States.Activated)
            {
                Attack();
                if (!ActivationRangeReached(3f))
                {
                    _currentState = States.Deactivated;
                }
            }
        }

        private void Attack()
        {
            if (_target == null)
            {
                return;
            }

            transform.LookAt(_target);
            _weapon.Shoot(FirePoint);
        }
    }
}