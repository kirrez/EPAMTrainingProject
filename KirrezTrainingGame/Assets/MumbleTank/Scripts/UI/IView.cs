using UnityEngine;

public interface IView
{
    void Show();
    void Hide();

    void SetParent(Transform parent);
    void SetPosition(Vector2 position);
}
