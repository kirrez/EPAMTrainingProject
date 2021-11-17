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
    public int bulletAmount = 3;
    public int maxClipSize = 10;
    public int shotsInClip = 10;

    public float CurrentCooldown { get; set; }
    public int BulletIndex { get; set; }
    public int CurrentClipSize { get; set; }
    public int ShotsLeft { get; set; }
    public int[] BulletStatus { get; set; }
    public GameObject[] Bullets { get; set; }

    // prefab references for bullets, blasts etc.
    public GameObject bulletPrefab;

    private void Awake()
    {
        Bullets = new GameObject[bulletAmount];
        BulletStatus = new int[bulletAmount];

        CurrentClipSize = maxClipSize;
        ShotsLeft = shotsInClip;

        if (bulletPrefab != null)
        {
            for (int i = 0; i < Bullets.Length; i++)
            {
                Bullets[i] = Instantiate(bulletPrefab);
                Bullets[i].transform.SetParent(gameObject.transform);
                SetupBulletProperties(Bullets[i]);
                Bullets[i].SetActive(false);
                BulletStatus[i] = 0;
            }
        }
        else
        {
            Debug.Log("Bullet Prefab Loading failed!");
        }
    }

    private void FixedUpdate()
    {
        if (CurrentCooldown > 0)
        {
            CurrentCooldown -= Time.fixedDeltaTime;
        }
        for (int i = 0; i < Bullets.Length; i++)
        {
            if (Bullets[i].GetComponent<Bullet>().FlightFinished == true)
            {
                BulletStatus[i] = 0;
                Bullets[i].SetActive(false);
                //outText1.text = $"BulletStatus : {_bulletStatus[0]} {_bulletStatus[1]} {_bulletStatus[2]}";
            }
        }
    }

    public virtual void SetupBulletProperties(GameObject target)
    {
        Bullet targetBullet = target.GetComponent<Bullet>();
        //if (targetBullet == null) Debug.Log("Bullet setup failed! (null)");
        targetBullet.SetLifetime(lifetime);
        targetBullet.SetBulletDamage(bulletDamage);
    }

    public void UpdateIndex()
    {
        if (BulletIndex < BulletStatus.Length - 1)
        {
            BulletIndex++;
        }
        else
        {
            BulletIndex = 0;
        }
    }

    public virtual void Shoot(Transform firePoint)
    {
        if ((BulletStatus[BulletIndex] == 0) && (CurrentCooldown <= 0))
        {
            if ((ShotsLeft == 0) && (CurrentClipSize == 0)) return;
            if ((ShotsLeft == 0) && (CurrentClipSize > 0))
            {
                // it's an instant reloading ))
                CurrentClipSize--;
                ShotsLeft = shotsInClip;
            }
            if ((ShotsLeft > 0) || (ShotsLeft < 0)) // < 0 for enemies since they have endless ammo
            {
                BulletStatus[BulletIndex] = 1; // set status to "flying"
                CurrentCooldown = shootingCooldown;
                Bullets[BulletIndex].SetActive(true);
                var rBody = Bullets[BulletIndex].GetComponent<Rigidbody>();
                rBody.transform.position = firePoint.position;
                rBody.transform.rotation = firePoint.rotation;
                rBody.velocity = rBody.transform.forward * bulletSpeed;
                UpdateIndex();

                ShotsLeft--;
            }
            //Debug.Log($"Clips : {CurrentClipSize} Shots : {ShotsLeft}");
        }
    }

    public void ForcedShoot(Transform firePoint)
    {
        CurrentCooldown = 0;
        if ((BulletStatus[BulletIndex] == 0) && (CurrentCooldown <= 0))
        {
            if ((ShotsLeft == 0) && (CurrentClipSize == 0)) return;
            if ((ShotsLeft == 0) && (CurrentClipSize > 0))
            {
                // it's an instant reloading ))
                CurrentClipSize--;
                ShotsLeft = shotsInClip;
            }
            if ((ShotsLeft > 0) || (ShotsLeft < 0)) // < 0 for enemies since they have endless ammo
            {
                BulletStatus[BulletIndex] = 1; // set status to "flying"
                CurrentCooldown = shootingCooldown;
                Bullets[BulletIndex].SetActive(true);
                var rBody = Bullets[BulletIndex].GetComponent<Rigidbody>();
                rBody.transform.position = firePoint.position;
                rBody.transform.rotation = firePoint.rotation;
                rBody.velocity = rBody.transform.forward * bulletSpeed;
                UpdateIndex();

                ShotsLeft--;
            }
            //Debug.Log($"Clips : {CurrentClipSize} Shots : {ShotsLeft}");
        }
    }
}
