using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Image TankNormal;
    public Image TankDamaged;
    public Image InvulnerabilityGauge;
    public RectTransform emptyBar; // prefab
    public RectTransform fullBar; // prefab
    
    public RectTransform[] EmptyBars { get; set; }
    public RectTransform[] FullBars { get; set; }

    public RectTransform HPGaugeBackground;
    public RectTransform HPGaugeFront;

    private int _maxHP = 0;
    private int _currentHP = 0;
    private float _invulnerabilityPeriod = 0f;
    private float _currentPeriod = 0f; // used in FixedUpdate for calculating of gauge fillAmount

    public bool Invulnerability { get; set; } = false; // upper class can access it for determination of possibility being damaged again

    private void Awake()
    {
        // setting Tank icons into base undamaged state
        TankNormal.gameObject.SetActive(true);
        TankDamaged.gameObject.SetActive(false);
        InvulnerabilityGauge.gameObject.SetActive(true);
        InvulnerabilityGauge.fillAmount = 0f;
    }

    private void FixedUpdate()
    {
        // performing display of invulnerability
        if (Invulnerability)
        {
            _currentPeriod -= Time.fixedDeltaTime;
            InvulnerabilityGauge.fillAmount = 1 / (_invulnerabilityPeriod / _currentPeriod);
            if (_currentPeriod <= 0)
            {
                Invulnerability = false;
                TankDamaged.gameObject.SetActive(false);
            }
        }
    }

    public void ActivateInvulnerability()
    {
        if (Invulnerability == false)
        {
            Invulnerability = true;
            _currentPeriod = _invulnerabilityPeriod;
            TankDamaged.gameObject.SetActive(true);
        }
    }

    public void SetMaxHP(int amount)
    {
        if (amount >= 1) _maxHP = amount;
        else _maxHP = 1;
        EmptyBars = new RectTransform[_maxHP];
        for (int i = 0; i < EmptyBars.Length; i++)
        {
            EmptyBars[i] = Instantiate(emptyBar);
            EmptyBars[i].SetParent(HPGaugeBackground);
        }

        // the same initialization procedure for currentHP and full bars
        if (amount >= 1) _currentHP = amount;
        else _currentHP = 1;
        FullBars = new RectTransform[_currentHP];
        for (int i = 0; i < FullBars.Length; i++)
        {
            FullBars[i] = Instantiate(fullBar);
            FullBars[i].SetParent(HPGaugeFront);
        }
    }

    public void SetInvulnerabilityPeriod(float period)
    {
        if (period < 0.1f) _invulnerabilityPeriod = 0.1f;
        else _invulnerabilityPeriod = period;
    }

    public void UpdateCurrentHP(int amount)
    {
        _currentHP = Mathf.Clamp(amount, 0, _maxHP);
        for (int i = 0; i < FullBars.Length; i++)
        {
            if (i < _currentHP)
            {
                FullBars[i].gameObject.SetActive(true);
            }
            if (i >= _currentHP)
            {
                FullBars[i].gameObject.SetActive(false);
            }
        }
    }

}
