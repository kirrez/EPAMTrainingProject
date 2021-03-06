using UnityEngine;

namespace TankGame
{
    public class HealthPresenter : MonoBehaviour
    {
        public Vector3 Offset;

        private IHealth _health;
        private IHealthBar _healthBar;
        private IGameCamera _gameCamera;
        //private IUIRoot _uiRoot;

        private void Awake()
        {
            var uiRoot = ServiceLocator.GetUIRoot();
            var resourceManager = ServiceLocator.GetResourceManager();
            _gameCamera = ServiceLocator.GetGameCamera();

            _health = GetComponent<IHealth>();
            _health.HealthChanged += OnHealthChanged;
            _health.MaxHealthChanged += OnMaxHealthChanged;

            _healthBar = resourceManager.CreatePrefab<IHealthBar, Widgets>(Widgets.HealthBar);
            _healthBar.SetParent(uiRoot.OverlayCanvas);
        }

        private void OnEnable()
        {
            InitializeHealthBar();

            _healthBar.Show();
        }

        private void Update()
        {
            UpdateHealthBarPosition(transform.position + Offset);
        }

        private void InitializeHealthBar()
        {
            _healthBar.SetMaxHealth(_health.MaxHealth);
            _healthBar.SetHealth(_health.CurrentHealth);

            UpdateHealthBarPosition(transform.position);
        }

        private void UpdateHealthBarPosition(Vector3 worldPosition)
        {
            var normalizedPosition = _gameCamera.WorldToViewportPoint(worldPosition);

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
}