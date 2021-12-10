using UnityEngine;

namespace TankGame
{
    public class UIRoot : MonoBehaviour, IUIRoot
    {
        public Transform OverlayCanvasLink;
        public Transform MainCanvasLink;
        public Transform MenuCanvasLink;

        public Transform OverlayCanvas
        {
            get
            {
                return OverlayCanvasLink;
            }
        }

        public Transform MainCanvas
        {
            get
            {
                return MainCanvasLink;
            }
        }

        public Transform MenuCanvas
        {
            get
            {
                return MenuCanvasLink;
            }
        }
    }
}