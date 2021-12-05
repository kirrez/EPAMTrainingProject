using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : BaseEnemy
{
    public Transform FirePoint;

    private float _moveSpeed = 8f;
    private float _preparingTime = 0f;
    private float _maxPreparingTime = 2.5f;

    private Vector3 _direction;
    private States _currentState;

    private enum States
    {
        Idle,
        Activated
    }

    private void OnEnable()
    {
        _currentState = States.Idle;
    }

    private void FixedUpdate()
    {
        if (_currentState == States.Idle)
        {
            Move(false);
        }
        if (_currentState == States.Idle && ActivationRangeReached(1f))
        {
            _currentState = States.Activated;
        }
        if (_currentState == States.Activated)
        {
            Move(true);
        }
    }

    private void Move(bool active)
    {
        if (_target == null)
        {
            return;
        }

        if (_preparingTime > 0)
        {
            _preparingTime -= Time.fixedDeltaTime;
            transform.LookAt(transform.position + _direction);
            var offset = transform.position + transform.forward * _moveSpeed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(offset);

            if (active == true)
            {
                _weapon.ForcedShoot(FirePoint);
            }
        }

        if (_preparingTime <= 0)
        {
            _maxPreparingTime = UnityEngine.Random.Range(1f, 3f);
            _preparingTime = _maxPreparingTime;
            // direction change
            if (active == false)
            {
                int newDirection = UnityEngine.Random.Range(0, 7);
                _direction = CopmassDirection(newDirection);
            }

            if (active == true)
            {
                var chance = UnityEngine.Random.Range(0f, 1f);

                if (chance < 0.5f)
                {
                    // random direction
                    int newDirection = UnityEngine.Random.Range(0, 7);
                    _direction = CopmassDirection(newDirection);
                }
                if (chance >= 0.5f)
                {
                    // following player
                    _direction = _target.position - transform.position;
                }
            }
        }
    }

    private Vector3 CopmassDirection(int dir)
    {
        switch (dir)
        {
            case 0:
                return new Vector3(0f, 0f, 1f);
            case 1:
                return new Vector3(1f, 0f, 1f);
            case 2:
                return new Vector3(1f, 0f, 0f);
            case 3:
                return new Vector3(1f, 0f, -1f);
            case 4:
                return new Vector3(0f, 0f, -1f);
            case 5:
                return new Vector3(-1f, 0f, -1f);
            case 6:
                return new Vector3(-1f, 0f, 0f);
            case 7:
                return new Vector3(-1f, 0f, 1f);
            default:
                return new Vector3(0f, 0f, 1f);
        }
    }
}
