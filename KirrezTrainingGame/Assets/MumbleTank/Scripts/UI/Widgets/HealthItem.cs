using UnityEngine;
using UnityEngine.UI;

namespace TankGame
{
    public class HealthItem : BaseView, IHealthItem
    {
        public Image Foreground;

        public void Fill()
        {
            Foreground.gameObject.SetActive(true);
        }

        public void Empty()
        {
            Foreground.gameObject.SetActive(false);
        }
    }
}