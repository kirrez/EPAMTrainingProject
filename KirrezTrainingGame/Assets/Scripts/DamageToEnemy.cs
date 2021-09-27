using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToEnemy : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player's bullet")
        {
            //Destroy(gameObject);
            //Destroy(other.gameObject);
        }
    }
}
