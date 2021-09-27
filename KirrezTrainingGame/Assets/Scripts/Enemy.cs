using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private float _maxHitPoints = 0f;
    private float _currentHitPoints = 0f;

    private EnemyProperties unitData;

    public Text outText;

    private void Awake()
    {
        unitData = GetComponent<EnemyProperties>();
    }

    private void Start()
    {
        SetupUnitData();
    }

    private void Update()
    {
        // Debug
        outText.text = "Enemy HP :" + _currentHitPoints.ToString("0.");
    }

    private void SetupUnitData()
    {
        unitData.SetHitPoints(out _maxHitPoints);
        _currentHitPoints = _maxHitPoints;
    }

    public void ReceiveBulletDamage(float damage)
    {
        _currentHitPoints -= damage;
        if (_currentHitPoints < 0)
        {
            _currentHitPoints = 0;
        }
    }
}
