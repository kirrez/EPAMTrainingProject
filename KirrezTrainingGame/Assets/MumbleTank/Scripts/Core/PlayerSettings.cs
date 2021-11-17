using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public static int UserMaxHitpoints = 3;
    public readonly int MaxHitpointsAvailable = 7;

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
