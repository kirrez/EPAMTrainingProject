using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankExplosion : MonoBehaviour
{
    public float force = 20f;
    public Transform explosionCenter;
    public GameObject tank;
    private Collider[] shards;

    private void OnEnable()
    {
        ActivateExplosion();
    }

    private void ActivateExplosion()
    {
        shards = Physics.OverlapSphere(explosionCenter.position, 5, 6);
        foreach (var shard in shards)
        {
            var direction = explosionCenter.position - shard.transform.position;
            shard.GetComponent<Rigidbody>().AddForce(direction * force);
        }
        //Destroy(tank, 3.5f);
    }
}
