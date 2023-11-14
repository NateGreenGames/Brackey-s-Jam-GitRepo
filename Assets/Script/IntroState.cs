using UnityEngine;

[System.Serializable]
public class IntroState : MonsterBaseState
{
    public float breathingRoomTimer;

    public override void Enrage(MonsterStateManager _monsterStateManager)
    {
        throw new System.NotImplementedException();
    }

    public override void EnterState(MonsterStateManager _monsterStateManager)
    {
        //PLAY VOICE OVER
        Debug.Log("Intro Clip Plays");
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
