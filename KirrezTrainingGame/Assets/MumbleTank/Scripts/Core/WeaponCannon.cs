using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class WeaponCannon : Weapon
    {
        public int piercingAmount = 1;

        public override void SetupBulletProperties(GameObject target)
        {
            base.SetupBulletProperties(target);
            Bullet targetBullet = target.GetComponent<Bullet>();
            targetBullet.SetPiercing(piercingAmount);
        }
    }
}