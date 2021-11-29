using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    public event Action<int> HealthChanged;
    public event Action<int> MaxHealthChanged;

    public event Action<bool> ShieldChanged;
    public event Action<float> ShieldProgress;

    public event Action Killed;

    public bool PlayerAlive { get; set; } = true;
    public int Health { get; set; }
    public int MaxHealth { get; set; }

    public Transform firePoint;

    private bool _isShielded;
    private float _shieldTimer = 0;

    private int _weaponIndex = 0;

    private List<Weapon> _weaponsList = new List<Weapon>();

    private Transform _targetForEnemy;

    private ExplodeEffect _explodeEffect;
    private BlinkingEffect _blinkingEffect;

    private IPlayerSettings _playerSettings;
    private IResourceManager _resourceManager;

    private void Awake()
    {
        _playerSettings = ServiceLocator.GetPlayerSettings();
        _resourceManager = ServiceLocator.GetResourceManager();

        _explodeEffect = GetComponent<ExplodeEffect>();
        _blinkingEffect = GetComponent<BlinkingEffect>();

        AddWeapon(Weapons.Machinegun);
        AddWeapon(Weapons.Spray);
        AddWeapon(Weapons.Cannon);

    }

    private void Start()
    {
        Health = _playerSettings.StartHealth;
        MaxHealth = _playerSettings.StartHealth;

        HealthChanged?.Invoke(Health);
        MaxHealthChanged?.Invoke(MaxHealth);
        ShieldChanged.Invoke(false);

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

            if (Input.GetButton("Fire1"))
            {
                if (_weaponsList[_weaponIndex] != null) _weaponsList[_weaponIndex].Shoot(firePoint);
            }
        }
    }

    public void ReceiveDamage()
    {
        if (_isShielded == true)
        {
            return;
        }

        Health--;

        HealthChanged?.Invoke(Health);

        if (Health == 0)
        {
            _explodeEffect.Explode();
            PlayerAlive = false;
            Killed?.Invoke();

            return;
        }

        _blinkingEffect.StartBlinking();
        _shieldTimer = _playerSettings.ShieldTime;
        _isShielded = true;

        ShieldChanged?.Invoke(true);
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
            _blinkingEffect.StopBlinking();
            _isShielded = false;

            ShieldChanged?.Invoke(false);
        }
        else
        {
            var progress = _shieldTimer / _playerSettings.ShieldTime;

            ShieldProgress?.Invoke(progress);
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

    public Transform GetTransform()
    {
        return transform;
    }

    public void SetStartPosition(Vector3 position)
    {
        transform.position = position;
    }
}
