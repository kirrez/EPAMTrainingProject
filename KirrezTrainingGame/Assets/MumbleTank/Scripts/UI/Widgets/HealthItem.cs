using UnityEngine;
using UnityEngine.UI;

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