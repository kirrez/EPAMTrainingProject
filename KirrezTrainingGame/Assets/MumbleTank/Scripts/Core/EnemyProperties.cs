using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties : MonoBehaviour
{
    public EnemyType type;
    public float hitPoints;
    public float moveSpeed;
    public float activationRadius;

    public Transform firepoint;
    public Weapon weaponPrefab;
    public GameObject deathExplosion;
    //public GameObject firepoint;

    //public void SetHitPoints(out float hp)
    //{
    //    hp = hitPoints;
    //}
}
