using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpray : Weapon
{
    public float Angle = 25f;

    public void ResetBullet()
    {
        BulletStatus[BulletIndex] = 0;
        Bullets[BulletIndex].SetActive(false);
    }

    public override void Shoot(Transform firePoint)
    {
        Rigidbody rBody;
        Vector3 direction;

        if (CurrentCooldown <= 0)
        {
            if ((ShotsLeft == 0) && (CurrentClipSize == 0)) return;
            if ((ShotsLeft == 0) && (CurrentClipSize > 0))
            {
                // it's an instant reloading ))
                CurrentClipSize--;
                ShotsLeft = shotsInClip;
            }
            if (ShotsLeft > 0)
            {
                // first bullet
                ResetBullet();
                BulletStatus[BulletIndex] = 1;
                Bullets[BulletIndex].SetActive(true);

                rBody = Bullets[BulletIndex].GetComponent<Rigidbody>();
                rBody.transform.position = firePoint.position;
                direction = firePoint.transform.forward;
                direction = Quaternion.AngleAxis(-Angle, Vector3.up) * direction;
                rBody.velocity = direction * bulletSpeed;
                UpdateIndex();

                //second bullet
                ResetBullet();
                BulletStatus[BulletIndex] = 1;
                Bullets[BulletIndex].SetActive(true);

                rBody = Bullets[BulletIndex].GetComponent<Rigidbody>();
                rBody.transform.position = firePoint.position;
                rBody.transform.rotation = firePoint.rotation;
                rBody.velocity = rBody.transform.forward * bulletSpeed;
                UpdateIndex();

                //third bullet
                ResetBullet();
                BulletStatus[BulletIndex] = 1;
                Bullets[BulletIndex].SetActive(true);

                rBody = Bullets[BulletIndex].GetComponent<Rigidbody>();
                rBody.transform.position = firePoint.position;
                direction = firePoint.transform.forward;
                direction = Quaternion.AngleAxis(Angle, Vector3.up) * direction;
                rBody.velocity = direction * bulletSpeed;
                UpdateIndex();
                //
                CurrentCooldown = shootingCooldown;

                ShotsLeft--;
            }
        }
    }
}
