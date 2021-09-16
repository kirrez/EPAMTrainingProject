using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAimingControl : MonoBehaviour
{
    public Transform tankTurret;
    public Camera myCamera;

    Vector3 lookDirection;

    private void FixedUpdate()
    {
        TurretDirectionUpdate();
    }

    private void TurretDirectionUpdate()
    {
        Ray mouseRay = myCamera.ScreenPointToRay(Input.mousePosition);
        float midPoint = (transform.position - myCamera.transform.position).magnitude * 0.8f;
        lookDirection = mouseRay.origin + mouseRay.direction * midPoint;
        lookDirection.y = tankTurret.position.y;
        tankTurret.LookAt(lookDirection);
    }
}
