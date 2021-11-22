using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    private IPauseMenu _pauseMenu;

    private void Awake()
    {
        _pauseMenu = new PauseMenu();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            _pauseMenu.Show();
        }
    }
}
