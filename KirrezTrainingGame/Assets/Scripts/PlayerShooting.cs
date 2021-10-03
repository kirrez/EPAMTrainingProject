using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public Weapon[] WeaponPrefabs = new Weapon[3]; // prefabs
    public Transform firePoint;
    public PlayerHUD playerHUD; // prefab

    public int maxHitpoints = 5;
    public int CurrentHitpoints { get; set; }
    public float invulnerabilityPeriod = 1.5f; // time before player can be damaged again
    
    private int _weaponIndex = 0;
    private Weapon[] Weapons { get; set; }
    //private PlayerHUD PlayerHUD;


    private void Awake()
    {
        Weapons = new Weapon[WeaponPrefabs.Length];

        for (int i = 0; i < WeaponPrefabs.Length; i++)
        {
            if (WeaponPrefabs[i] != null)
            {
                Weapons[i] = Instantiate(WeaponPrefabs[i], transform.position, transform.rotation);
                Weapons[i].transform.SetParent(gameObject.transform);
            }
        }
        // player always starts with full HP
        CurrentHitpoints = maxHitpoints;
    }

    private void Start()
    {
        playerHUD.SetMaxHP(maxHitpoints);
        playerHUD.SetInvulnerabilityPeriod(invulnerabilityPeriod);
    }

    private void Update()
    {
        // choosing weapons
        if (Input.GetKey(KeyCode.Alpha1) && Weapons.Length >= 1) _weaponIndex = 0;
        if (Input.GetKey(KeyCode.Alpha2) && Weapons.Length >= 2) _weaponIndex = 1;
        if (Input.GetKey(KeyCode.Alpha3) && Weapons.Length >= 3) _weaponIndex = 2;
        if (Input.GetKey(KeyCode.Alpha4) && Weapons.Length >= 4) _weaponIndex = 3;
        if (Input.GetKey(KeyCode.Alpha5) && Weapons.Length >= 5) _weaponIndex = 4;

        //debug
        if (Input.GetKeyUp(KeyCode.Space) && (playerHUD.Invulnerability == false))
        {
            CurrentHitpoints--;
            playerHUD.UpdateCurrentHP(CurrentHitpoints);
            playerHUD.ActivateInvulnerability();
        }

        if (Input.GetButton("Fire1"))
        {
            if (Weapons[_weaponIndex] != null) Weapons[_weaponIndex].Shoot(firePoint);
        }
    }


}
