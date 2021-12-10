using UnityEngine;

namespace TankGame
{
    public class BlinkingItem : MonoBehaviour
    {
        private Renderer _renderer;
        private Color _defaultColor;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _defaultColor = _renderer.material.color;

            var effect = GetComponentInParent<BlinkingEffect>();
            effect.RegisterItem(this);
        }

        public void SetColor(Color color)
        {
            _renderer.material.color = color;
        }

        public void RevertColor()
        {
            _renderer.material.color = _defaultColor;
        }
    }
}