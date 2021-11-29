using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties : MonoBehaviour
{
    public EnemyType type;
    public float hitPoints;
    public float moveSpeed;
    public float activationRadius;
    public int score;

    public Weapon weaponPrefab;
}
