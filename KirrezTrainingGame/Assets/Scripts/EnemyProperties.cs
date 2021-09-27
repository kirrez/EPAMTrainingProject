using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties : MonoBehaviour
{
    public float hitPoints;

    public void SetHitPoints(out float hp)
    {
        hp = hitPoints;
    }
}
