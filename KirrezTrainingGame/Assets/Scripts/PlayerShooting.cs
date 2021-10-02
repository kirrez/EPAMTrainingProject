using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public Weapon[] WeaponPrefabs = new Weapon[3]; // prefabs
    public Transform firePoint;

    public int maxHitpoints = 5;

    private int _currentHitpoints = 0;
    private int _weaponIndex = 0;
    private Weapon[] _weapons;


    private void Awake()
    {
        _weapons = new Weapon[WeaponPrefabs.Length];

        for (int i = 0; i < WeaponPrefabs.Length; i++)
        {
            if (WeaponPrefabs[i] != null)
            {
                _weapons[i] = Instantiate(WeaponPrefabs[i], transform.position, transform.rotation);
                _weapons[i].transform.SetParent(gameObject.transform);
            }
        }

        _currentHitpoints = maxHitpoints;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1) && _weapons.Length >= 1) _weaponIndex = 0;

        if (Input.GetKey(KeyCode.Alpha2) && _weapons.Length >= 2) _weaponIndex = 1;

        if (Input.GetKey(KeyCode.Alpha3) && _weapons.Length >= 3) _weaponIndex = 2;

        if (Input.GetKey(KeyCode.Alpha4) && _weapons.Length >= 4) _weaponIndex = 3;

        if (Input.GetKey(KeyCode.Alpha5) && _weapons.Length >= 5) _weaponIndex = 4;

        if (Input.GetButton("Fire1"))
        {
            if (_weapons[_weaponIndex] != null) _weapons[_weaponIndex].Shoot(firePoint);
        }
    }
}
