using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementControl : MonoBehaviour
{
    public float moveSpeed = 3f;

    public Transform tankBody;

    new Rigidbody rigidbody;

    Vector3 direction;
    Vector3 bodyDirection = new Vector3(0f, 0f, 1f);
    float bodyAngle = 0f;

    void Start()
    {
        direction = Vector3.zero;
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        bodyDirection = direction;

        if (bodyDirection != Vector3.zero)
        {
            bodyAngle = Vector3.SignedAngle(new Vector3(0f, 0f, 1f), bodyDirection, new Vector3(0f, 1f, 0f));
            tankBody.rotation = Quaternion.Euler(0f, bodyAngle, 0f);
        }
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + direction * moveSpeed * Time.deltaTime);
    }
}
