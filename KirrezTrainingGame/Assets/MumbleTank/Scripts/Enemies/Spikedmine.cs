using UnityEngine;

public class Spikedmine : BaseEnemy
{
    private enum States
    {
        Idle,
        Activated,
        CloseContact
    }

    public Transform Body;

    private States _currentState;
    private Quaternion startRotation = Quaternion.AngleAxis(1f, Vector3.up);

    private void OnEnable()
    {
        _currentState = States.Idle;
    }

    private void FixedUpdate()
    {
        if (_currentState == States.Idle && ActivationRangeReached(1f))
        {
            _currentState = States.Activated;
        }

        if (_currentState == States.Activated)
        {
            Move(false);
        }

        if (ActivationRangeReached(0.2f))
        {
            _currentState = States.CloseContact;
        }

        if (_currentState == States.CloseContact)
        {
            Move(true);
        }
    }

    private void Move(bool acceleration)
    {
        if (_target == null)
        {
            return;
        }

        transform.LookAt(_target);

        var moveSpeed = _unitData.moveSpeed;

        if (acceleration)
        {
            moveSpeed *= 2;
        }

        var offset = transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(offset);

        var newRotation = Quaternion.AngleAxis(2f, Vector3.up);
        startRotation *= newRotation;
        Body.rotation = startRotation;
    }
}