using System;
using UnityEngine;

namespace TankGame
{
    public abstract class BaseEnemy : MonoBehaviour, IHealth, IEnemy
    {
        public event Action<IEnemy> Died = enemy => { };
        public event Action<float> HealthChanged = value => { };
        public event Action<float> MaxHealthChanged = value => { };

        public float MaxHealth
        {
            get
            {
                return _maxHitPoints;
            }
        }

        public float CurrentHealth
        {
            get
            {
                return _currentHitPoints;
            }
        }

        public Vector3 Position
        {
            get
            {
                return transform.position;
            }
            set
            {
                transform.position = value;
            }
        }

        public Quaternion Rotation
        {
            get
            {
                return transform.rotation;
            }
            set
            {
                transform.rotation = value;
            }
        }

        public EnemyType Type { get; set; }

        protected Weapon _weapon;
        protected Transform _target;
        protected Rigidbody _rigidbody;
        protected Vector3 _startPosition;

        private IResourceManager _resourceManager;
        protected EnemyProperties _unitData;

        private int _score = 0;
        private float _moveSpeed = 8f;
        private float _maxHitPoints = 0f;
        private float _currentHitPoints = 0f;

        private float _activationRadius = float.MaxValue;

        private IUnitRepository _unitRepository;

        private void Awake()
        {
            var player = ServiceLocator.GetPlayer();
            _unitRepository = ServiceLocator.GetUnitRepository();
            _resourceManager = ServiceLocator.GetResourceManager();

            _target = player.GetTransform();
            _unitData = GetComponent<EnemyProperties>();
            _rigidbody = GetComponent<Rigidbody>();

            SetupUnitData();

            if (_unitData.weaponPrefab != null)
            {
                _weapon = Instantiate(_unitData.weaponPrefab, transform.position, transform.rotation);
            }
        }

        protected virtual void OnEnable()
        {
            _unitRepository.Register(this);
            SetupUnitData();
        }

        private void OnDisable()
        {
            _unitRepository.Unregister(this);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "PlayerTank")
            {
                var player = collision.gameObject.GetComponentInParent<Player>();
                player.ReceiveDamage();

                Die();
            }
        }

        private void Die()
        {
            _currentHitPoints = 0f;

            ShowDieEffect();

            HealthChanged.Invoke(_currentHitPoints);
            Died.Invoke(this);
        }

        protected virtual void SetupUnitData()
        {
            Type = _unitData.type;

            _score = _unitData.score;
            _maxHitPoints = _unitData.hitPoints;
            _currentHitPoints = _maxHitPoints;
            _moveSpeed = _unitData.moveSpeed;

            MaxHealthChanged.Invoke(_maxHitPoints);
            HealthChanged.Invoke(_currentHitPoints);

            _activationRadius = _unitData.activationRadius;
        }

        protected bool ActivationRangeReached(float k)
        {
            if (_target == null)
            {
                return false;
            }

            var distance = transform.position - _target.position;

            if (distance.magnitude <= _activationRadius * k) return true;
            else return false;
        }

        public void ReceiveDamage(float damage)
        {
            _currentHitPoints -= damage;

            HealthChanged.Invoke(_currentHitPoints);

            if (_currentHitPoints < 0)
            {
                Die();
            }
        }

        public void ShowDieEffect()
        {
            var explosion = _resourceManager.CreatePrefab<Effects>(Effects.Explosion);
            explosion.transform.position = transform.position;
            Destroy(explosion, 1f);

            gameObject.SetActive(false);
        }

        public int GetScore()
        {
            return _score;
        }

        public bool IsEnabled()
        {
            return gameObject.activeSelf;
        }

        public void Enable()
        {
            gameObject.SetActive(true);
            SetupUnitData();
        }

        public void DiscardTarget()
        {
            _target = null;
        }
    }
}