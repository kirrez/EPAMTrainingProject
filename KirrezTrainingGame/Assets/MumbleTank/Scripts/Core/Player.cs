using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    public event Action<int> HealthChanged;

    public bool PlayerAlive { get; set; } = true;
    public int CurrentHitpoints { get; set; }

    public Transform firePoint;
    public int maxHitpoints = 3;
    public float ShieldTime = 1.5f; // time before player can be damaged again

    private bool _isShielded;
    private float _shieldTimer = 0;

    private int _weaponIndex = 0;

    private List<Weapon> _weaponsList = new List<Weapon>();

    private BlinkingEffect Effects;
    private IResourceManager _resourceManager;

    private void Awake()
    {
        _resourceManager = ServiceLocator.GetResourceManager();
        Effects = GetComponent<BlinkingEffect>();

        AddWeapon(Weapons.Machinegun);
        AddWeapon(Weapons.Spray);
        AddWeapon(Weapons.Cannon);

        // player always starts with full HP
        maxHitpoints = GetComponent<PlayerSettings>().GetMaxHitpoints(); // gameObject with "PlayerShooting" script must also have "PlayerSettings" script on it
        CurrentHitpoints = maxHitpoints;
    }

    private void Start()
    {
        //playerHUD.SetMaxHP(maxHitpoints);
        //playerHUD.SetInvulnerabilityPeriod(invulnerabilityPeriod);
    }

    private void Update()
    {
        // choosing weapons
        if (PlayerAlive)
        {
            if (Input.GetKey(KeyCode.Alpha1) && _weaponsList.Count >= 1) _weaponIndex = 0;
            if (Input.GetKey(KeyCode.Alpha2) && _weaponsList.Count >= 2) _weaponIndex = 1;
            if (Input.GetKey(KeyCode.Alpha3) && _weaponsList.Count >= 3) _weaponIndex = 2;
            if (Input.GetKey(KeyCode.Alpha4) && _weaponsList.Count >= 4) _weaponIndex = 3;
            if (Input.GetKey(KeyCode.Alpha5) && _weaponsList.Count >= 5) _weaponIndex = 4;

            //debug
            if (Input.GetKeyUp(KeyCode.Space))
            {
                ReceiveDamage();
            }

            if (Input.GetButton("Fire1"))
            {
                if (_weaponsList[_weaponIndex] != null) _weaponsList[_weaponIndex].Shoot(firePoint);
            }
        }
    }

    public void ReceiveDamage()
    {
        //if ((PlayerAlive) && (playerHUD.Invulnerability == false))
        //{
        CurrentHitpoints--;

        HealthChanged?.Invoke(CurrentHitpoints);

        //playerHUD.UpdateCurrentHP(CurrentHitpoints);

        if (CurrentHitpoints == 0)
        {
            Effects.Explode();
            PlayerAlive = false;

            return;
        }

        //playerHUD.ActivateInvulnerability();

        Effects.StartBlinking();
        _shieldTimer = ShieldTime;
        _isShielded = true;

        //}
    }

    private void FixedUpdate()
    {
        if (_isShielded == false)
        {
            return;
        }

        if (_shieldTimer > 0f)
        {
            _shieldTimer -= Time.fixedDeltaTime;
        }

        if (_shieldTimer <= 0)
        {
            Effects.StopBlinking();
            _isShielded = false;
        }
    }

    private void AddWeapon(Weapons type)
    {
        var weapon = _resourceManager.CreatePrefab<Weapon, Weapons>(type);
        weapon.transform.position = transform.position;
        weapon.transform.rotation = transform.rotation;
        weapon.transform.SetParent(transform);

        _weaponsList.Add(weapon);
    }
}
