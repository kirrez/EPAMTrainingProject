using System;
using UnityEngine.UI;

public class OptionsMenuView : BaseView, IOptionsMenuView
{
    public event Action AddLivesClicked = () => { };
    public event Action DecreaseLivesClicked = () => { };
    public event Action BackClicked = () => { };

    public Button AddLivesButton;
    public Button DecreaseLivesButton;
    public Button BackToMenuButton;

    public Text LivesAmountText;

    private void Awake()
    {
        AddLivesButton.onClick.AddListener(OnAddLivesClicked);
        DecreaseLivesButton.onClick.AddListener(OnDecreaseLivesClicked);
        BackToMenuButton.onClick.AddListener(OnBackClicked);
    }

    private void OnAddLivesClicked()
    {
        AddLivesClicked();
    }

    private void OnDecreaseLivesClicked()
    {
        DecreaseLivesClicked();
    }

    private void OnBackClicked()
    {
        BackClicked();
    }

    public void SetHealth(int value)
    {
        LivesAmountText.text = value.ToString();
    }
}