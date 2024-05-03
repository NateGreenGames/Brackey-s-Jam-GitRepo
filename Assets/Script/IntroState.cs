using UnityEngine;

[System.Serializable]
public class IntroState : MonsterBaseState
{
    public float breathingRoomTimer;
    private string saveDataKey = "HasPlayedIntro";

    public override void Enrage(MonsterStateManager _monsterStateManager)
    {
        throw new System.NotImplementedException();
    }

    public override void EnterState(MonsterStateManager _monsterStateManager)
    {
        if (!PlayerPrefs.HasKey(saveDataKey))
        {
            PlayerPrefs.SetInt(saveDataKey, 1);
            //PLAY VOICE OVER
            Debug.Log("Intro Clip Plays");
            IntroClipSubManager.introInstance.StartCoroutine(IntroClipSubManager.introInstance.GenerateSubtitles());
        }
        else
        {
            //Don't play the intro stuff and instead end the breathing room timer and drain the correct amount of electricity.
            //Drain power based on the default state of breathingRoomTimer;
            ElectricityManager.ChangeElectricityAmount(-breathingRoomTimer / 4f);
            breathingRoomTimer = 0;
            Debug.Log("Intro Skipped");
        }
    }

    public override void OnDisable()
    {
        throw new System.NotImplementedException();
    }

    public override void OnEnable()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(MonsterStateManager _monsterStateManager)
    {
        breathingRoomTimer -= Time.deltaTime;
        if (breathingRoomTimer <= 0)
        {
            _monsterStateManager.SwitchStates(_monsterStateManager.idleState);
        }
    }
}
