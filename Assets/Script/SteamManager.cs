using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamManager : MonoBehaviour
{
    public static SteamManager instance = null;
    private uint appID = 3008570;
    private int totalNumberOfAchievements = 10;
    private bool connectedToSteam = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }


        try
        {
            Steamworks.SteamClient.Init(appID);
            connectedToSteam = true;
        }
        catch(System.Exception exception)
        {
            connectedToSteam = false;
        }
    }


    private void Update()
    {
        Steamworks.SteamClient.RunCallbacks();
    }



    public void DisconnectFromSteam()
    {
        if(connectedToSteam == true)
        {
            Steamworks.SteamClient.Shutdown();
        }
    }


    public void UnlockAchievement(int _AchievmentIndex)
    {
        if(connectedToSteam == true)
        {
            var ach = new Steamworks.Data.Achievement("Achievement_" + _AchievmentIndex);
            ach.Trigger();
            CheckForPlatinumAchievement();
        }
    }

    private void CheckForPlatinumAchievement()
    {
        int numberOfAchievementsRequired = totalNumberOfAchievements - 1;
        int numberOfUnlockedAchievemnets = 0;

        for (int i = 0; i < numberOfAchievementsRequired; i++)
        {
            var ach = new Steamworks.Data.Achievement("Achievement_" + i);
            if(ach.State == true)
            {
                numberOfUnlockedAchievemnets++;
            }
        }
        if(numberOfUnlockedAchievemnets == numberOfAchievementsRequired)
        {
            var ach = new Steamworks.Data.Achievement("Achievement_" + 9);
            ach.Trigger();
        }
    }

}
