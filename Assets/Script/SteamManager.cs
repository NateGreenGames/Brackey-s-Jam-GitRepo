using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eAchievement { Intro, DieToOxygen, DieToPower, DieToWall, Win, SurviveEye, SurviveWorm, SurviveTentacle, DieToMonster, Platinum }
public class SteamManager : MonoBehaviour
{
    public static SteamManager instance;
    [NamedArray(typeof(eAchievement))] [SerializeField] bool[] localAchievementStates;

    private const uint appID = 3008570;
    private const int totalNumberOfAchievements = 10;
    private bool connectedToSteam = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        try
        {
            Steamworks.SteamClient.Init(appID);
            connectedToSteam = true;
        }
        catch (System.Exception exception)
        {
            connectedToSteam = false;
        }

        if (connectedToSteam)
        {
            //ResetAllAchievements();
            PopulateLocalAchievementStates();
        }
    }


    void Update()
    {
        if (connectedToSteam)
        {
            Steamworks.SteamClient.RunCallbacks();
        }
    }


    public void DisconnectFromSteam()
    {
        if (connectedToSteam)
        {
            Steamworks.SteamClient.Shutdown();
        }
    }


    public void UnlockAchievement(eAchievement _AchievementToUnlock)
    {
        if (connectedToSteam)
        {
            var ach = new Steamworks.Data.Achievement("Achievement_" + (int)_AchievementToUnlock);
            ach.Trigger();
            UpdateLocalAchievementStates(_AchievementToUnlock);
            CheckForPlatinumAchievement();
        }
    }

    private void UpdateLocalAchievementStates(eAchievement _AchievementEarned)
    {
        localAchievementStates[(int)_AchievementEarned] = true;
    }
    private void PopulateLocalAchievementStates()
    {
        localAchievementStates = new bool[totalNumberOfAchievements];
        for (int i = 0; i < totalNumberOfAchievements; i++)
        {
            var ach = new Steamworks.Data.Achievement("Achievement_" + i);
            localAchievementStates[i] = ach.State;
        }
    }
    private void CheckForPlatinumAchievement()
    {
        if (!localAchievementStates[(int)eAchievement.Platinum])
        {
            int numberOfAchievementsRequired = totalNumberOfAchievements - 1;
            int numberOfUnlockedAchievementsFound = 0;

            for (int i = 0; i < numberOfAchievementsRequired; i++)
            {
                Debug.Log("Checking Achievement:" + i);
                if (localAchievementStates[i] == true)
                {
                    numberOfUnlockedAchievementsFound++;
                }
                else
                {
                    break;
                }
            }

            if (numberOfUnlockedAchievementsFound == numberOfAchievementsRequired)
            {
                UnlockAchievement(eAchievement.Platinum);
            }
        }
    }
    private void ResetAllAchievements()
    {
        for (int i = 0; i < totalNumberOfAchievements; i++)
        {
            var ach = new Steamworks.Data.Achievement("Achievement_" + i);
            ach.Clear();
        }
    }
}
