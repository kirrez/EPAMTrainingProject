public class PlayerSettings : IPlayerSettings
{
    public const int MaxHitpointsAvailable = 7;

    private int UserMaxHitpoints = 3;

    public void SetHitpoints(bool increase)
    {
        if ((increase) && (UserMaxHitpoints < MaxHitpointsAvailable)) UserMaxHitpoints++;
        if ((!increase) && (UserMaxHitpoints > 1)) UserMaxHitpoints--;
    }

    public int GetMaxHitpoints()
    {
        return UserMaxHitpoints;
    }
}