using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint;

    public Weapon weapon;
    //private Weapon weapon1prefab;
    //private Weapon weapon;

    private void Awake()
    {
        //weapon1prefab = Resources.Load("Prefabs/Weapon1") as Weapon;
        //Debug.Log(weapon1prefab.ToString());
        //weapon = Instantiate(weapon1prefab) as Weapon;
    }

    private void Update()
    {
        //if (Input.GetButton("Fire1") && Time.time > _nextFire)
        //{
        //    _nextFire = Time.time + fireRate;
        //    // пока что упрощенно
        //    Instantiate(bullet, firePoint.position, firePoint.rotation);
        //}

        if (Input.GetButton("Fire1"))
        {
            weapon.Shoot(firePoint);
        }
    }
}
