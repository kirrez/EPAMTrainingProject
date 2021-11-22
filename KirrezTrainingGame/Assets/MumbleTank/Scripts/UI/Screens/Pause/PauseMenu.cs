using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : IPauseMenu
{
    private IPauseMenuView _view;

    public PauseMenu()
    {
        var uiRoot = ServiceLocator.GetUIRoot();
        var resourceManager = ServiceLocator.GetResourceManager();

        _view = resourceManager.CreatePrefab<IPauseMenuView, Views>(Views.Pause);
        _view.SetParent(uiRoot.MenuCanvas);

        _view.ResumeClicked += OnResumeClicked;
        _view.RestartClicked += OnRestartClicked;
        _view.BackClicked += OnBackClicked;
    }

    public void Show()
    {
        _view.Show();
        Time.timeScale = 0f;
    }

    public void Hide()
    {
        _view.Hide();
        Time.timeScale = 1f;
    }

    private void OnResumeClicked()
    {
        Hide();
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
}
