using System.Collections.Generic;
using UnityEngine;

public class BlinkingEffect : MonoBehaviour
{
    public GameObject Mesh;
    public Color BlinkingColor;
    public float BlinkingFrequency = 0.2f;

    private IResourceManager _resourceManager;

    private bool _isBlinking;
    private List<BlinkingItem> _items = new List<BlinkingItem>();

    private void Awake()
    {
        _resourceManager = ServiceLocator.GetResourceManager();
    }

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
    }

    public void Explode()
    {
        Mesh.SetActive(false);

        var explodingTank = _resourceManager.CreatePrefab<GameObject, PlayerComponents>(PlayerComponents.ExplodingTank);
        explodingTank.transform.SetParent(transform, false);
    }
}