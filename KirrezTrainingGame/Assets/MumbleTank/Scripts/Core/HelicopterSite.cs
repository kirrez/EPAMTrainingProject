using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class HelicopterSite : MonoBehaviour
    {
        public Action IsReached = () => { };

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "PlayerTank")
            {
                IsReached.Invoke();
            }
        }
    }
}
