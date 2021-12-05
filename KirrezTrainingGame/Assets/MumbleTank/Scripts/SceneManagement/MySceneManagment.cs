using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManagment : MonoBehaviour
{
    private static Scenes _menuScene = Scenes.Menu;

    public void RestartLevel()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(_menuScene.ToString());
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}