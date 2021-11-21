//using System;
//using UnityEngine;
//using UnityEngine.UI;

//public class Enemy : MonoBehaviour, IHealth, IEnemy
//{
//    public event Action<IEnemy> Died;
//    public event Action<float> HealthChanged;
//    public event Action<float> MaxHealthChanged;

//    public float MaxHealth
//    {
//        get
//        {
//            return _maxHitPoints;
//        }
//    }

//    public float CurrentHealth
//    {
//        get
//        {
//            return _currentHitPoints;
//        }
//    }

//    public Vector3 Position
//    {
//        get
//        {
//            return transform.position;
//        }
//        set
//        {
//            transform.position = value;
//        }
//    }

//    public Quaternion Rotation
//    {
//        get
//        {
//            return transform.rotation;
//        }
//        set
//        {
//            transform.rotation = value;
//        }
//    }

//    public Transform model; // link for self-rotating spikedmine
//    public EnemyType Type { get; set; }
//    public HealthBar HealthBar { get; set; } = null;

//    private Transform firepoint;
//    private Weapon weapon;
//    private Transform _target;
//    private Rigidbody _rigidbody;
//    private Vector3 _direction;
//    private GameObject _deathExplosionPrefab = null;
//    private EnemyProperties unitData;
//    private EnemyState state;

//    private Vector3 startPosition;
//    private float _preparingTime = 0f;
//    private float _maxPreparingTime = 2.5f;
//    private float _maxHitPoints = 0f;
//    private float _currentHitPoints = 0f;
//    private float _moveSpeed = 8f;
//    private float _activationRadius = float.MaxValue;
//    private IOverlayCanvas _overlayCanvas;

//    private Quaternion startRotation = Quaternion.AngleAxis(1f, Vector3.up);

//    private void Awake()
//    {
//        _overlayCanvas = ServiceLocator.GetOverlayCanvas();

//        unitData = GetComponent<EnemyProperties>();
//        _rigidbody = GetComponent<Rigidbody>();
//        _direction = Vector3.zero;

//        if (unitData.weaponPrefab != null)
//        {
//            weapon = Instantiate(unitData.weaponPrefab, transform.position, transform.rotation);
//        }

//        if (unitData.firepoint != null)
//        {
//            firepoint = unitData.firepoint;
//        }

//    }

//    private void OnEnable()
//    {
//        SetupUnitData();
//        state = EnemyState.Idle;

//        // учтено нулл
//        if (HealthBar != null)
//        {
//            //HealthBar.Initialize(_maxHitPoints);
//            HealthBar.SetMaxHealth(_maxHitPoints);
//            HealthBar.ResetHealth();
//        }
//    }

//    private void FixedUpdate()
//    {
//        switch (Type)
//        {
//            case EnemyType.Spikedmine:
//                SpikedmineUpdate();
//                break;
//            case EnemyType.Turret:
//                TurretUpdate();
//                break;
//            case EnemyType.Flyer:
//                FlyerUpdate();
//                break;
//        }
//    }

//    private void TurretUpdate()
//    {
//        if (state == EnemyState.Idle && ActivationRangeReached(1f))
//        {
//            state = EnemyState.Preparing;
//            _preparingTime = _maxPreparingTime;
//            startPosition = transform.position;
//        }

//        if (state == EnemyState.Preparing)
//        {
//            if (_preparingTime >= 0)
//            {
//                _preparingTime -= Time.fixedDeltaTime;
//                var finishPosition = transform.position;
//                finishPosition.y = 6f;
//                transform.position = Vector3.Lerp(startPosition, finishPosition, (1 - _preparingTime) / _maxPreparingTime);
//            }
//            else
//            {
//                state = EnemyState.Activated;
//            }
//        }

//        if (state == EnemyState.Deactivated && ActivationRangeReached(1f))
//        {
//            state = EnemyState.Activated;
//        }

//        if (state == EnemyState.Activated)
//        {
//            TurretAttack();
//            if (!ActivationRangeReached(3f))
//            {
//                state = EnemyState.Deactivated;
//            }
//        }
//    }

//    private void SpikedmineUpdate()
//    {
//        if (state == EnemyState.Idle && ActivationRangeReached(1f))
//        {
//            state = EnemyState.Activated;
//        }

//        if (state == EnemyState.Activated)
//        {
//            SpikedMovement(false);
//        }

//        if (ActivationRangeReached(0.2f))
//        {
//            state = EnemyState.CloseContact;
//        }

//        if (state == EnemyState.CloseContact)
//        {
//            SpikedMovement(true);
//        }
//    }

//    private void FlyerUpdate()
//    {
//        if (state == EnemyState.Idle)
//        {
//            FlyerMovement(false);
//        }
//        if (state == EnemyState.Idle && ActivationRangeReached(1f))
//        {
//            state = EnemyState.Activated;
//        }
//        if (state == EnemyState.Activated)
//        {
//            FlyerMovement(true);
//        }
//    }

//    private void SpikedMovement(bool acceleration)
//    {
//        if (_target == null)
//        {
//            return;
//        }

//        transform.LookAt(_target);
//        var moveSpeed = _moveSpeed;

//        if (acceleration)
//        {
//            moveSpeed *= 2;
//        }

//        var offset = transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime;
//        _rigidbody.MovePosition(offset);

//        var newRotation = Quaternion.AngleAxis(2f, Vector3.up);
//        startRotation *= newRotation;
//        model.rotation = startRotation;
//    }

//    private void TurretAttack()
//    {
//        if (_target == null)
//        {
//            return;
//        }

//        transform.LookAt(_target);
//        weapon.Shoot(firepoint);
//    }

//    private void FlyerMovement(bool active)
//    {
//        if (_target == null)
//        {
//            return;
//        }

//        if (_preparingTime > 0)
//        {
//            _preparingTime -= Time.fixedDeltaTime;
//            transform.LookAt(transform.position + _direction);
//            var offset = transform.position + transform.forward * _moveSpeed * Time.fixedDeltaTime;
//            _rigidbody.MovePosition(offset);

//            if (active == true)
//            {
//                weapon.ForcedShoot(firepoint);
//            }
//        }

//        if (_preparingTime <= 0)
//        {
//            _maxPreparingTime = UnityEngine.Random.Range(1f, 3f);
//            _preparingTime = _maxPreparingTime;
//            // direction change
//            if (active == false)
//            {
//                int newDirection = UnityEngine.Random.Range(0, 7);
//                _direction = CopmassDirection(newDirection);
//            }

//            if (active == true)
//            {
//                var chance = UnityEngine.Random.Range(0f, 1f);

//                if (chance < 0.5f)
//                {
//                    // random direction
//                    int newDirection = UnityEngine.Random.Range(0, 7);
//                    _direction = CopmassDirection(newDirection);
//                }
//                if (chance >= 0.5f)
//                {
//                    // following player
//                    _direction = _target.position - transform.position;
//                }
//            }
//        }
//    }

//    private void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.tag == "PlayerTank")
//        {
//            var player = collision.gameObject.GetComponentInParent<Player>();
//            player.ReceiveDamage();

//            Die();
//        }
//    }

//    private void Die()
//    {
//        _currentHitPoints = 0f;

//        ShowDieEffect();

//        HealthChanged?.Invoke(_currentHitPoints);
//        Died?.Invoke(this);
//    }

//    // some enemies falls from sky on your head, another jumps from below your feet
//    public static float CorrectYPosition(EnemyType type)
//    {
//        float newY = 0f;
//        switch (type)
//        {
//            case EnemyType.Spikedmine:
//                newY = 10f;
//                break;
//            case EnemyType.Turret:
//                newY = -4f;
//                break;
//            case EnemyType.Flyer:
//                newY = 2f;
//                break;
//        }
//        return newY;
//    }

//    public void SetTarget(Transform target)
//    {
//        _target = target;
//    }

//    private void SetupUnitData()
//    {
//        Type = unitData.type;

//        _maxHitPoints = unitData.hitPoints;
//        _currentHitPoints = _maxHitPoints;

//        MaxHealthChanged?.Invoke(_maxHitPoints);
//        HealthChanged?.Invoke(_currentHitPoints);

//        _moveSpeed = unitData.moveSpeed;
//        _activationRadius = unitData.activationRadius;

//        _deathExplosionPrefab = unitData.deathExplosion;
//    }

//    private bool ActivationRangeReached(float k)
//    {
//        if (_target == null)
//        {
//            return false;
//        }

//        // "normal == false" for Spikedmines and their CloseContact state
//        var distance = transform.position - _target.position;
//        if (distance.magnitude <= _activationRadius * k) return true;
//        // 5 - magic number for acceleration
//        //if (!normal && distance.magnitude <= 5) return true;
//        else return false;
//    }

//    public void ReceiveDamage(float damage)
//    {
//        _currentHitPoints -= damage;

//        HealthChanged?.Invoke(_currentHitPoints);

//        //if (HealthBar != null) HealthBar.UpdateHealth(_currentHitPoints);

//        if (_currentHitPoints < 0)
//        {
//            Die();
//        }
//    }

//    public void ShowDieEffect()
//    {
//        if (_deathExplosionPrefab != null)
//        {
//            var explosion = Instantiate(_deathExplosionPrefab);
//            explosion.transform.position = transform.position;
//            Destroy(explosion, 1f);
//        }
//        gameObject.SetActive(false);
//    }

//    private Vector3 CopmassDirection(int dir)
//    {
//        switch (dir)
//        {
//            case 0:
//                return new Vector3(0f, 0f, 1f);
//            case 1:
//                return new Vector3(1f, 0f, 1f);
//            case 2:
//                return new Vector3(1f, 0f, 0f);
//            case 3:
//                return new Vector3(1f, 0f, -1f);
//            case 4:
//                return new Vector3(0f, 0f, -1f);
//            case 5:
//                return new Vector3(-1f, 0f, -1f);
//            case 6:
//                return new Vector3(-1f, 0f, 0f);
//            case 7:
//                return new Vector3(-1f, 0f, 1f);
//            default:
//                return new Vector3(0f, 0f, 1f);
//        }
//    }
//}
