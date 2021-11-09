using UnityEngine;

public class HealthPresenter : MonoBehaviour
{
    private Camera _camera;
    private IHealth _health;
    private IHealthBar _healthBar;

    public Vector3 Offset;

    private void Awake()
    {
        var game = FindObjectOfType<Game>();
        _camera = game.GetCamera();
        _health = GetComponent<IHealth>();

        InitializeHealthBar();

        _health.HealthChanged += OnHealthChanged;
        _health.MaxHealthChanged += OnMaxHealthChanged;
    }

    private void OnEnable()
    {
        _healthBar.Show();
    }

    private void Update()
    {
        UpdateHealthBarPosition(transform.position + Offset);
    }

    private void InitializeHealthBar()
    {
        var canvas = FindObjectOfType<OverlayCanvas>();
        var prefab = Resources.Load<GameObject>("Prefabs/UI/HealthBar");
        var instance = Instantiate(prefab);
        _healthBar = instance.GetComponent<IHealthBar>();

        _healthBar.SetParent(canvas.transform);
        _healthBar.SetMaxHealth(_health.MaxHealth);
        _healthBar.SetHealth(_health.CurrentHealth);

        UpdateHealthBarPosition(transform.position);
    }

    private void UpdateHealthBarPosition(Vector3 worldPosition)
    {
        var normalizedPosition = _camera.WorldToViewportPoint(worldPosition);

        normalizedPosition.x -= 0.5f;
        normalizedPosition.y -= 0.5f;

        var screenPosition = new Vector2(normalizedPosition.x * Screen.width, normalizedPosition.y * Screen.height);

        _healthBar.SetPosition(screenPosition);
    }

    private void OnHealthChanged(float value)
    {
        _healthBar.SetHealth(value);

        if (value <= 0f)
        {
            _healthBar.Hide();
        }
    }

    private void OnMaxHealthChanged(float value)
    {
        _healthBar.SetMaxHealth(value);
    }
}
