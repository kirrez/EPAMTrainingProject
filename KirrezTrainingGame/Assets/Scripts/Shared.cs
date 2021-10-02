using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// All directions in XZ plane (Z instead of Y)
public static class Shared
{
    public static Vector3 North = new Vector3(0f, 0f, 1f);
    public static Vector3 NorthEast = new Vector3(1f, 0f, 1f);
    public static Vector3 East = new Vector3(1f, 0f, 0f);
    public static Vector3 SouthEast = new Vector3(1f, 0f, -1f);
    public static Vector3 South = new Vector3(0f, 0f, -1f);
    public static Vector3 SouthWest = new Vector3(-1f, 0f, -1f);
    public static Vector3 West = new Vector3(-1f, 0f, 0f);
    public static Vector3 NorthWest = new Vector3(-1f, 0f, 1f);

    public static float AngleFromVector(Vector3 direction)
    {
        direction = direction.normalized;
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;
        return angle;
    }

    public static Vector3 VectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        Vector3 vector = new Vector3(Mathf.Cos(angleRad), 0f, Mathf.Sin(angleRad));
        return vector;
    }
}
