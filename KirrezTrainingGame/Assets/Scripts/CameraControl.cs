using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform myCamera;
    public Transform targetPlayer; // player's tank to follow to

    private float smooth = 5f;

    void Start()
    {

    }

    void Update()
    {
        if (transform.position != targetPlayer.position)
        {
            transform.position = Vector3.Lerp(transform.position, targetPlayer.position, Time.deltaTime * smooth);
        }
    }
}
