using System;

public interface IMainMenuView : IView
{
    event Action StartGameClicked;
    event Action OptionsClicked;
    event Action QuitClicked;
}