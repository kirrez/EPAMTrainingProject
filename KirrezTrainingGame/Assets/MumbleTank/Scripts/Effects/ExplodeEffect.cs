using UnityEngine;

namespace TankGame
{
    public class ExplodeEffect : MonoBehaviour
    {
        public GameObject Mesh;

        private IResourceManager _resourceManager;

        private void Awake()
        {
            _resourceManager = ServiceLocator.GetResourceManager();
        }

        public void Explode()
        {
            Mesh.SetActive(false);

            var explodingTank = _resourceManager.CreatePrefab<Transform, PlayerComponents>(PlayerComponents.ExplodingTank);
            explodingTank.position = transform.position;
            explodingTank.rotation = transform.rotation;
        }
    }
}