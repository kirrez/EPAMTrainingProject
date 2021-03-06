using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame
{
    public class GameHUDView : BaseView, IGameHUDView
    {
        public Image TankNormalIcon;
        public Image TankDamagedIcon;
        public Image ShieldProgress;
        public Text TaskDescription;

        public Transform HealthContent;
        private List<IHealthItem> HealthItems = new List<IHealthItem>();

        private IResourceManager _resourceManager;

        private void Awake()
        {
            _resourceManager = ServiceLocator.GetResourceManager();
        }

        public void SetTaskDescription(string description)
        {
            TaskDescription.text = description;
        }

        public void SetTaskDescription(string description, float value)
        {
            var output = string.Format(description, value);
            TaskDescription.text = output;
        }

        public void SetTaskDescription(string description, int current, int maximum)
        {
            var output = string.Format(description, current, maximum);
            TaskDescription.text = output;
        }

        public void SetShieldActive(bool isActive)
        {
            ShieldProgress.fillAmount = 0f;
            TankDamagedIcon.gameObject.SetActive(isActive);
        }

        public void SetShieldProgress(float normalizedValue)
        {
            ShieldProgress.fillAmount = normalizedValue;
        }

        public void SetHealth(int value)
        {
            for (int i = 0; i < HealthItems.Count; i++)
            {
                var item = HealthItems[i];

                if (value > i)
                {
                    item.Fill();
                }
                else
                {
                    item.Empty();
                }
            }
        }

        public void SetMaxHealth(int value)
        {
            foreach (var item in HealthItems)
            {
                item.Hide();
            }

            for (int i = 0; i < value; i++)
            {
                IHealthItem item;

                if (HealthItems.Count > i)
                {
                    item = HealthItems[i];
                }
                else
                {
                    item = _resourceManager.CreatePrefab<IHealthItem, Widgets>(Widgets.HealthItem);
                    item.SetParent(HealthContent);
                }

                item.Show();
                item.Fill();

                HealthItems.Add(item);
            }
        }
    }
}