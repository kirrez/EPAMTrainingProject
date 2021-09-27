using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public bool flightFinished = false; // accesible for Weapon class

    private float maxLifetime = 3f; // get from Weapon class
    private float currentLifetime = 0f;
    private float bulletDamage = 1f; // get from Weapon class

    private void OnEnable()
    {
        //Debug.Log("Bullet enabled!");
        currentLifetime = maxLifetime;
        flightFinished = false;
    }

    private void FixedUpdate()
    {
        if (currentLifetime > 0)
        {
            currentLifetime -= Time.fixedDeltaTime;
        }
        if (currentLifetime <= 0)
        {
            flightFinished = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy Unit")
        {
            Enemy target = other.GetComponent<Enemy>();
            Debug.Log("Hit enemy!");
            target.ReceiveBulletDamage(bulletDamage);
        }
    }

    public void SetLifetime(float lifetime)
    {
        if (lifetime <= 0)
        {
            maxLifetime = 0.01f;
        }
        else
        {
            maxLifetime = lifetime;
        }
    }

    public void SetBulletDamage(float damage)
    {
        if (damage <= 1f)
        {
            bulletDamage = 1f;
        }
        else
        {
            bulletDamage = damage;
        }
        Debug.Log($"bullet damage is {bulletDamage}");
    }
}
