using UnityEngine;

namespace TankGame
{
    public class TankExplosion : MonoBehaviour
    {
        public float force = 20f;
        public Transform explosionCenter;
        private Collider[] shards;

        private void OnEnable()
        {
            ActivateExplosion();
        }

        private void ActivateExplosion()
        {
            int layerId = 6;
            int layerMask = 1 << layerId;

            shards = Physics.OverlapSphere(explosionCenter.position, 5, layerMask);

            foreach (var shard in shards)
            {
                var direction = explosionCenter.position - shard.transform.position;
                shard.GetComponent<Rigidbody>().AddForce(direction * force);
            }
        }
    }
}