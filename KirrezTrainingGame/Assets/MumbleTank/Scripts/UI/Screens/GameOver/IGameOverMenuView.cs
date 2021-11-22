using System;


public interface IGameOverMenuView : IView
{
    event Action RestartClicked;
    event Action BackClicked;
    event Action QuitClicked;
}
