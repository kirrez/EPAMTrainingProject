using UnityEngine;

public class UIRoot : MonoBehaviour, IUIRoot
{
    public Transform MenuCanvasLink;
    public Transform OverlayCanvasLink;

    public Transform MenuCanvas
    {
        get
        {
            return MenuCanvasLink;
        }
    }

    public Transform OverlayCanvas
    {
        get
        {
            return OverlayCanvasLink;
        }
    }
}