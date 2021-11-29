using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRepository : MonoBehaviour, IUnitRepository
{
    public event Action<IEnemy> Killed = enemy => { };

    private List<IEnemy> _enemiesList = new List<IEnemy>();
    // for debug
    private int _killedAmount = 0;

    private void Awake()
    {
        Killed += Onkilled;
    }

    public void Register(IEnemy enemy)
    {
        _enemiesList.Add(enemy);
    }

    public void Unregister(IEnemy enemy)
    {
        _enemiesList.Remove(enemy);
        Killed.Invoke(enemy);
    }

    // debug
    private void Onkilled(IEnemy enemy)
    {
        _killedAmount++;
        Debug.Log($"Enemies killed : {_killedAmount}");
    }
}
