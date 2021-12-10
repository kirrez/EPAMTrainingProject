using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame
{
    public abstract class Weapon : MonoBehaviour
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

        public ShellType shell;

        protected IResourceManager _resourceManager;

        protected void Awake()
        {
            _resourceManager = ServiceLocator.GetResourceManager();

            CurrentClipSize = maxClipSize;
            ShotsLeft = shotsInClip;

        }

        protected void FixedUpdate()
        {
            if (CurrentCooldown > 0)
            {
                CurrentCooldown -= Time.fixedDeltaTime;
            }
        }

        public virtual void SetupBulletProperties(GameObject target)
        {
            Bullet targetBullet = target.GetComponent<Bullet>();
            targetBullet.SetLifetime(lifetime);
            targetBullet.SetBulletDamage(bulletDamage);
        }

        public virtual void Shoot(Transform firePoint)
        {
            if (CurrentCooldown <= 0)
            {
                if ((ShotsLeft == 0) && (CurrentClipSize == 0)) return;
                if ((ShotsLeft == 0) && (CurrentClipSize > 0))
                {
                    // instant reloading
                    CurrentClipSize--;
                    ShotsLeft = shotsInClip;
                }

                if ((ShotsLeft > 0) || (ShotsLeft < 0))
                    {
                    var bullet = _resourceManager.GetFromPool(shell);
                    //Debug.Log("seccusful!");
                    var rBody = bullet.GetComponent<Rigidbody>();

                    CurrentCooldown = shootingCooldown;
                    SetupBulletProperties(bullet);
                    rBody.transform.position = firePoint.position;
                    rBody.transform.rotation = firePoint.rotation;
                    rBody.velocity = rBody.transform.forward * bulletSpeed;
                    ShotsLeft--;
                    }
            }
        }
    }
}