
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : IGameOverMenu
{
    private IGameOverMenuView _view;

    public GameOverMenu()
    {
        var uiRoot = ServiceLocator.GetUIRoot();
        var resourceManager = ServiceLocator.GetResourceManager();

        _view = resourceManager.CreatePrefab<IGameOverMenuView, Views>(Views.GameOver);
        _view.SetParent(uiRoot.MenuCanvas);

        _view.RestartClicked += OnRestartClicked;
        _view.BackClicked += OnBackClicked;
        _view.QuitClicked += OnQuitClicked;
    }

    public void Show()
    {
        _view.Show();
    }

    public void Hide()
    {
        _view.Hide();
    }

    private void OnRestartClicked()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    private void OnBackClicked()
    {
        SceneManager.LoadScene(Scenes.Menu.ToString());
    }

    private void OnQuitClicked()
    {
        Application.Quit();
    }
}
