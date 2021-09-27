using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public float shootingCooldown = 0.5f;
    public float bulletSpeed = 20f;
    public float lifetime = 2f;
    public float bulletDamage = 20f;

    private float _currentCooldown = 0f; // when shooting it gets value of "shootingCooldown", and then delta time is subtracting
    private GameObject[] _bullets = new GameObject[3];
    private int[] _bulletStatus = new int[3];  // 0 - ready for shooting, 1 - flying
    private int _bulletIndex = 0;

    // prefab reference
    private GameObject bullet;

    public Text outText;
    public Text outText1;

    private void Awake()
    {
        bullet = Resources.Load("Prefabs/Shell") as GameObject;
        if (bullet == null) Debug.Log("Loading failed");
        for (int i = 0; i < _bullets.Length; i++)
        {
            _bullets[i] = Instantiate(bullet);
            SetupBulletProperties(_bullets[i]);
            _bullets[i].SetActive(false);
            _bulletStatus[i] = 0;
        }
    }

    private void FixedUpdate()
    {
        // checking shot's cooldown
        if (_currentCooldown > 0)
        {
            _currentCooldown -= Time.fixedDeltaTime;
            outText.text = _currentCooldown.ToString("0.00");
        }
        for (int i = 0; i < _bullets.Length; i++)
        {
            if (_bullets[i].GetComponent<Bullet>().flightFinished == true)
            {
                _bulletStatus[i] = 0; // set status to "ready for shooting"
                _bullets[i].SetActive(false);
                outText1.text = $"BulletStatus : {_bulletStatus[0]} {_bulletStatus[1]} {_bulletStatus[2]}";
            }
        }
    }

    public void SetupBulletProperties(GameObject target)
    {
        Bullet targetBullet = target.GetComponent<Bullet>();
        if (targetBullet == null) Debug.Log("Bullet setup failed! (null)");
        targetBullet.SetLifetime(lifetime);
        targetBullet.SetBulletDamage(bulletDamage);
    }

    public void UpdateIndex()
    {
        if (_bulletIndex < _bulletStatus.Length - 1)
        {
            _bulletIndex++;
        }
        else
        {
            _bulletIndex = 0;
        }
    }

    public void Shoot(Transform firePoint)
    {
        if ((_bulletStatus[_bulletIndex] == 0) && (_currentCooldown <= 0))
        {
            _bulletStatus[_bulletIndex] = 1; // set status to "flying"
            outText1.text = $"BulletStatus : {_bulletStatus[0]} {_bulletStatus[1]} {_bulletStatus[2]}"; //debug
            _currentCooldown = shootingCooldown;
            _bullets[_bulletIndex].SetActive(true);
            var rBody = _bullets[_bulletIndex].GetComponent<Rigidbody>();
            rBody.transform.position = firePoint.position;
            rBody.transform.rotation = firePoint.rotation;
            rBody.velocity = rBody.transform.forward * bulletSpeed;
            //Debug.Log($"FIRE!! index {_bulletIndex}");
            UpdateIndex();
        }
    }
}
