using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class scoreTracker
{
    // Track time it took player to complete each game
    public static float dogTime = 0.0f;
    public static float sprinklerTime = 0.0f;
    public static float mailbagTime = 0.0f;
    public static int umbrellaUse = 0;

    // Track whether or not they won each game
    public static bool dogWin = false;
    public static bool sprinklerWin = false;
    public static bool mailbagWin = false;


    public static void reset()
    {
        dogTime = 0.0f;
        sprinklerTime = 0.0f;
        mailbagTime = 0.0f;
        umbrellaUse = 0;

        dogWin = false;
        sprinklerWin = false;
        mailbagWin = false;
    }
}
