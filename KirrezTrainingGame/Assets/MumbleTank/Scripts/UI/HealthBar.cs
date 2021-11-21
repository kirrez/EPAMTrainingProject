using UnityEngine;
using UnityEngine.UI;

public class HealthBar : BaseView, IHealthBar
{
    private Slider slider;
    private float _maxHealth;
    private float _currentHealth;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetHealth(float health)
    {
        _currentHealth = health;

        if (_currentHealth <= 0)
        {
            slider.value = 0f;
        }
        else
        {
            slider.value = _currentHealth / _maxHealth;
        }
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
        slider.value = 1f;
    }

    public void SetMaxHealth(float value)
    {
        if (value == 0f)
        {
            return;
        }

        _maxHealth = value;
        slider.value = _currentHealth / _maxHealth;

        if (_currentHealth <= 0) slider.value = 0f;
    }
}