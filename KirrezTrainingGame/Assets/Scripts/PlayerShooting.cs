using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public Transform tankBody;
    public Transform tankTurret;

    public Weapon[] WeaponPrefabs = new Weapon[3]; // prefabs
    public Transform firePoint;
    public PlayerHUD playerHUD; // still not prefab, but it sure will
    public GameUI gameUI; // still not prefab, but it sure will

    public int maxHitpoints = 3;
    public int CurrentHitpoints { get; set; }
    public float invulnerabilityPeriod = 1.5f; // time before player can be damaged again

    public bool PlayerAlive { get; set; } = true;

    private bool _tankBlinking = false; // for changing meshes
    private bool _blinkingEffect = false;
    private float _currentPeriod = 0f;
    private float _blinkingDelta = 0.2f;
    private float _secondTimer = 0;
    private int _weaponIndex = 0;
    private Weapon[] Weapons { get; set; }
    //private PlayerHUD PlayerHUD;

    private GameObject tankExplosionRes;
    
    public GameObject[] tankNormalMesh = new GameObject[4];
    public GameObject[] tankBlinkingMesh = new GameObject[4];

    public GameObject[] TankNormalMesh { get; set; } = new GameObject[4];
    public GameObject[] TankBlinkingMesh { get; set; } = new GameObject[4];

    private void Awake()
    {
        tankExplosionRes = Resources.Load<GameObject>("Prefabs/Tank Parts/TankShards");

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
        maxHitpoints = GetComponent<PlayerSettings>().GetMaxHitpoints(); // gameObject with "PlayerShooting" script must also have "PlayerSettings" script on it
        CurrentHitpoints = maxHitpoints;

        //Loading Tank parts.. (blinking effect)
        for (int i = 0; i < tankNormalMesh.Length; i++)
        {
            TankNormalMesh[i] = Instantiate(tankNormalMesh[i], transform.position, transform.rotation);
            TankBlinkingMesh[i] = Instantiate(tankBlinkingMesh[i], transform.position, transform.rotation);
            if (i < 3)
            {
                TankNormalMesh[i].transform.SetParent(tankBody);
                TankBlinkingMesh[i].transform.SetParent(tankBody);
            }
            if (i == 3)
            {
                TankNormalMesh[i].transform.SetParent(tankTurret);
                TankNormalMesh[i].transform.position = tankTurret.transform.position;
                TankBlinkingMesh[i].transform.SetParent(tankTurret);
                TankBlinkingMesh[i].transform.position = tankTurret.transform.position;
            }
        }
        // Here we'll instantiate from "playerHUD" and "gameUI" prefabs
        // ...
    }

    private void Start()
    {
        playerHUD.SetMaxHP(maxHitpoints);
        playerHUD.SetInvulnerabilityPeriod(invulnerabilityPeriod);

        SwitchTankMesh(_tankBlinking);
    }

    private void Update()
    {
        // choosing weapons
        if (PlayerAlive)
        {
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
                if (CurrentHitpoints == 0)
                {
                    ExplodeTank();
                    return;
                }
                playerHUD.ActivateInvulnerability();
                ActivateBlinkingEffect();
            }

            if (Input.GetButton("Fire1"))
            {
                if (Weapons[_weaponIndex] != null) Weapons[_weaponIndex].Shoot(firePoint);
            }
        }
    }

    private void FixedUpdate()
    {
        // blinking effect..
        if (_tankBlinking)
        {
            _currentPeriod -= Time.fixedDeltaTime;
            if (_currentPeriod <= 0)
            {
                _tankBlinking = false;
                SwitchTankMesh(_tankBlinking); // temporary
            }

            _secondTimer -= Time.fixedDeltaTime;
            if (_secondTimer <= 0)
            {
                _blinkingEffect = !_blinkingEffect;
                SwitchTankMesh(_blinkingEffect);
                _secondTimer = _blinkingDelta;
            }
        }
    }

    public void SwitchTankMesh(bool blinking)
    {
        if (blinking)
        {
            for (int i = 0; i < TankBlinkingMesh.Length; i++)
            {
                TankBlinkingMesh[i].SetActive(true);
                TankNormalMesh[i].SetActive(false);
            }
        }
        if (!blinking)
        {
            for (int i = 0; i < TankBlinkingMesh.Length; i++)
            {
                TankBlinkingMesh[i].SetActive(false);
                TankNormalMesh[i].SetActive(true);
            }
        }
    }

    private void ActivateBlinkingEffect()
    {
        if (_tankBlinking == false)
        {
            _tankBlinking = true;
            _blinkingEffect = true;
            _currentPeriod = invulnerabilityPeriod;
            SwitchTankMesh(_tankBlinking); // temporary
            _secondTimer = _blinkingDelta;
        }
    }

    private void ExplodeTank()
    {
        for (int i = 0; i < TankBlinkingMesh.Length; i++)
        {
            TankBlinkingMesh[i].SetActive(false);
            TankNormalMesh[i].SetActive(false);
        }
        var explosion = Instantiate(tankExplosionRes, transform, false);
        PlayerAlive = false;
        gameUI.ActivateGameoverScreen();
    }

}
