using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRepository : MonoBehaviour, IUnitRepository
{
    public event Action<IEnemy> Killed = enemy => { };

    private List<IEnemy> _enemiesList = new List<IEnemy>();

    private void Awake()
    {

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

    public void StopChasingPlayer()
    {
        foreach( IEnemy enemy in _enemiesList)
        {
            enemy.DiscardTarget();
        }
    }
}
