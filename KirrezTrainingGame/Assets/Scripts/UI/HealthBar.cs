using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    private float _maxHealth;
    private float _currentHealth;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void Initialize(float health)
    {
        _maxHealth = health;
        _currentHealth = _maxHealth;
        slider.value = 1f;
    }

    public void UpdateHealth(float health)
    {
        _currentHealth = health;
        slider.value = _currentHealth / _maxHealth;
        if (_currentHealth <= 0) slider.value = 0f;
    }
}
