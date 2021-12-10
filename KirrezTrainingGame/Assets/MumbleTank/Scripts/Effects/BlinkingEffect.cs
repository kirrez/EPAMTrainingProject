using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class BlinkingEffect : MonoBehaviour
    {
        public Color BlinkingColor;
        public float BlinkingFrequency = 0.2f;

        private bool _isBlinking;
        private List<BlinkingItem> _items = new List<BlinkingItem>();

        private void Update()
        {
            if (_isBlinking == false)
            {
                return;
            }

            var phase = (int)(Time.time / BlinkingFrequency) % 2;

            if (phase == 0)
            {
                foreach (var item in _items)
                {
                    item.SetColor(BlinkingColor);
                }
            }
            else
            {
                foreach (var item in _items)
                {
                    item.RevertColor();
                }
            }
        }

        public void RegisterItem(BlinkingItem item)
        {
            _items.Add(item);
        }

        public void StartBlinking()
        {
            _isBlinking = true;
        }

        public void StopBlinking()
        {
            _isBlinking = false;

            foreach (var item in _items)
            {
                item.RevertColor();
            }
        }
    }
}