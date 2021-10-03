using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _maxLifetime = 3f; // get from Weapon class
    private float _currentLifetime = 0f;
    private float _bulletDamage = 1f; // get from Weapon class
    private int _maxPiercingAmount = 0;
    private int _piercingAmount = 0;

    public bool FlightFinished { get; set; }

    private void OnEnable()
    {
        //Debug.Log("Bullet enabled!");
        _currentLifetime = _maxLifetime;
        _piercingAmount = _maxPiercingAmount;
        FlightFinished = false;
    }

    private void FixedUpdate()
    {
        if (_currentLifetime > 0)
        {
            _currentLifetime -= Time.fixedDeltaTime;
        }
        if (_currentLifetime <= 0)
        {
            FlightFinished = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy Unit")
        {
            Enemy target = other.GetComponent<Enemy>();
            Debug.Log("Hit enemy!");
            target.ReceiveBulletDamage(_bulletDamage);
            //_piercingAmount--;
            //// if value goes below zero, it means our weapon doesn't use piercing logic
            if (_piercingAmount == 0) FlightFinished = true;
            else _piercingAmount--;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }



    public void SetLifetime(float lifetime)
    {
        if (lifetime <= 0)
        {
            _maxLifetime = 0.01f;
        }
        else
        {
            _maxLifetime = lifetime;
        }
    }

    public void SetBulletDamage(float damage)
    {
        if (damage <= 1f)
        {
            _bulletDamage = 1f;
        }
        else
        {
            _bulletDamage = damage;
        }
        //Debug.Log($"bullet damage is {bulletDamage}");
    }

    public void SetPiercing(int piercing)
    {
        if (piercing < 0) _piercingAmount = 0;
        else _maxPiercingAmount = piercing;
    }
}