using System;

public interface IPauseMenu : IScreen
{
    event Action Resuming;
    event Action Restarting;
    event Action Backing;
}
