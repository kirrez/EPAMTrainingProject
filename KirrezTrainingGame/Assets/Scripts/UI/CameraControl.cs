using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Vector3 positionOffset = new Vector3(0f, 30f, 0f);

    private Transform _target;
    private float _smooth = 5f;

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
}
