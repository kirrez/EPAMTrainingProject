using System;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, IHealth, IEnemy
{
    public event Action<IEnemy> Died;
    public event Action<float> HealthChanged;
    public event Action<float> MaxHealthChanged;

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
    
    private GameObject _deathExplosionPrefab = null;
    protected EnemyProperties _unitData;

    protected Vector3 _startPosition;
    
    private float _moveSpeed = 8f;
    private float _maxHitPoints = 0f;
    private float _currentHitPoints = 0f;

    private float _activationRadius = float.MaxValue;

    protected virtual void Awake()
    {
        var player = ServiceLocator.GetPlayer();

        _target = player.GetTransform();
        _unitData = GetComponent<EnemyProperties>();
        _rigidbody = GetComponent<Rigidbody>();

        SetupUnitData();

        if (_unitData.weaponPrefab != null)
        {
            _weapon = Instantiate(_unitData.weaponPrefab, transform.position, transform.rotation);
        }
    }

    private void OnEnable()
    {
        SetupUnitData();
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

        HealthChanged?.Invoke(_currentHitPoints);
        Died?.Invoke(this);
    }

    // some enemies falls from sky on your head, another jumps from below your feet
    public static float CorrectYPosition(EnemyType type)
    {
        float newY = 0f;
        switch (type)
        {
            case EnemyType.Spikedmine:
                newY = 10f;
                break;
            case EnemyType.Turret:
                newY = -4f;
                break;
            case EnemyType.Flyer:
                newY = 2f;
                break;
        }
        return newY;
    }

    protected virtual void SetupUnitData()
    {
        Type = _unitData.type;

        _maxHitPoints = _unitData.hitPoints;
        _currentHitPoints = _maxHitPoints;
        _moveSpeed = _unitData.moveSpeed;

        MaxHealthChanged?.Invoke(_maxHitPoints);
        HealthChanged?.Invoke(_currentHitPoints);

        _activationRadius = _unitData.activationRadius;
    }

    protected bool ActivationRangeReached(float k)
    {
        if (_target == null)
        {
            return false;
        }

        // "normal == false" for Spikedmines and their CloseContact state
        var distance = transform.position - _target.position;
        if (distance.magnitude <= _activationRadius * k) return true;
        // 5 - magic number for acceleration
        //if (!normal && distance.magnitude <= 5) return true;
        else return false;
    }

    public void ReceiveDamage(float damage)
    {
        _currentHitPoints -= damage;

        HealthChanged?.Invoke(_currentHitPoints);

        if (_currentHitPoints < 0)
        {
            Die();
        }
    }

    public void ShowDieEffect()
    {
        if (_deathExplosionPrefab != null)
        {
            var explosion = Instantiate(_deathExplosionPrefab);
            explosion.transform.position = transform.position;
            Destroy(explosion, 1f);
        }
        gameObject.SetActive(false);
    }
}