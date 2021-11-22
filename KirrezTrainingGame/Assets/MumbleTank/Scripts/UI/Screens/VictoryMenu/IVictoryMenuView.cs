using System;

public interface IVictoryMenuView : IView
{
    event Action ResumeClicked;
    event Action NextLevelClicked;
}
