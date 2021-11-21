using System;

public interface IOptionsMenuView : IView
{
    event Action AddLivesClicked;
    event Action DecreaseLivesClicked;
    event Action BackClicked;

    void SetLivesAmount(int value);
}