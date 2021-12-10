using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class UnitRepository : MonoBehaviour, IUnitRepository
    {
        public event Action<IEnemy> Killed = enemy => { };

        private List<IEnemy> _enemiesList = new List<IEnemy>();

        private void OnEnemyDied(IEnemy enemy)
        {
            enemy.Died -= OnEnemyDied;
            Killed(enemy);
        }

        public void Register(IEnemy enemy)
        {
            _enemiesList.Add(enemy);
            enemy.Died += OnEnemyDied;
        }

        public void Unregister(IEnemy enemy)
        {
            _enemiesList.Remove(enemy);
        }

        public void StopChasingPlayer()
        {
            foreach (IEnemy enemy in _enemiesList)
            {
                enemy.DiscardTarget();
            }
        }
    }
}