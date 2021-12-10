using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class Bullet : MonoBehaviour
    {
        public BulletType type;

        private float _maxLifetime = 3f;
        private float _currentLifetime = 0f;
        private float _bulletDamage = 1f;
        private int _maxPiercingAmount = 0;
        private int _piercingAmount = 0;
        private GameObject hitSparclePrefab;
        private GameObject sparcle;

        //public bool FlightFinished { get; set; }

        private void Awake()
        {
            hitSparclePrefab = Resources.Load<GameObject>("Prefabs/HitEnemySparcle");
        }

        private void OnEnable()
        {
            //Debug.Log("Bullet enabled!");
            _currentLifetime = _maxLifetime;
            _piercingAmount = _maxPiercingAmount;
            //FlightFinished = false;
        }

        private void FixedUpdate()
        {
            if (_currentLifetime > 0)
            {
                _currentLifetime -= Time.fixedDeltaTime;
            }
            if (_currentLifetime <= 0)
            {
                //FlightFinished = true;
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((type == BulletType.PvE_bullet) && (other.tag == "Enemy Unit"))
            {
                BaseEnemy target = other.GetComponent<BaseEnemy>();
                target.ReceiveDamage(_bulletDamage);
                //Effect of enemy being hit
                sparcle = Instantiate(hitSparclePrefab, transform.position, Quaternion.identity);
                Destroy(sparcle, 0.5f);
                //// if value goes below zero, it means our weapon doesn't use piercing logic
                if (_piercingAmount == 0)
                {
                    //FlightFinished = true;
                    gameObject.SetActive(false);
                }
                else
                {
                    _piercingAmount--;
                }
            }
            if ((type == BulletType.EvP_bullet) && (other.tag == "PlayerTank"))
            {
                var player = other.GetComponentInParent<Player>();
                player.ReceiveDamage();

                //FlightFinished = true;
                gameObject.SetActive(false);
            }

            if (other.gameObject.layer == 7)
            {
                //FlightFinished = true;
                gameObject.SetActive(false);
            }
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

        }

        public void SetPiercing(int piercing)
        {
            if (piercing < 0) _piercingAmount = 0;
            else _maxPiercingAmount = piercing;
        }
    }
}
