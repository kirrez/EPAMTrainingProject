
using System;

public interface IPauseMenuView : IView
{

    event Action ResumeClicked;
    event Action RestartClicked;
    event Action BackClicked;
}
