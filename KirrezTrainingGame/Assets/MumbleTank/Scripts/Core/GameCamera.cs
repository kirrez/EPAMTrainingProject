using UnityEngine;

public class GameCamera : MonoBehaviour, IGameCamera
{
    public Vector3 positionOffset = new Vector3(0f, 30f, 0f);

    private Transform _target;
    private float _smooth = 5f;
    private Camera _cameraComponent;

    private void Awake()
    {
        _cameraComponent = GetComponent<Camera>();
    }

    public Vector3 Position
    {
        get
        {
            return transform.position;
        }
    }

    public Ray ScreenPointToRay(Vector2 position)
    {
        var ray = _cameraComponent.ScreenPointToRay(position);

        return ray;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if (transform.position != _target.position + positionOffset)
        {
            transform.position = Vector3.Lerp(transform.position, _target.position + positionOffset, Time.deltaTime * _smooth);
        }
    }

    public Vector3 WorldToViewportPoint(Vector3 position)
    {
        var point = _cameraComponent.WorldToViewportPoint(position);

        return point;
    }
}
