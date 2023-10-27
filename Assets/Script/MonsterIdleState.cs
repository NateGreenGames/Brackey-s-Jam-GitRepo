using System.Collections;
using UnityEngine;

[System.Serializable]
public class MonsterIdleState : MonsterBaseState
{
    public float attackRate = 0;
    public float attackRateIncrease = 3f;
    public int monsterIdx;
    public bool rigged;

    public override void OnEnable()
    {
        //Do nothings
    }

    public override void OnDisable()
    {
        //Do nothing
    }

    public override void EnterState(MonsterStateManager _monsterStateManager)
    {
        Debug.Log("Monster manager entered IdleState");
    }

    public override void UpdateState(MonsterStateManager _monsterStateManager)
    {
        WaitForAttackSequence();
        if (attackRate >= 100)
        {
            Debug.Log("Start Attack");
            attackRate = 0;
            switch (RandomNumber())
            {
                case 1:
                    _monsterStateManager.SwitchStates(_monsterStateManager.eyeballState);
                    break;
                case 2:
                    _monsterStateManager.SwitchStates(_monsterStateManager.tentacleMonsterState);
                    break;
                case 3:
                    _monsterStateManager.SwitchStates(_monsterStateManager.wormMonsterState);
                    break;
                default:
                    break;
            }
        }
    }
    public void WaitForAttackSequence()
    {
        attackRate += Time.deltaTime * attackRateIncrease;
    }

    int RandomNumber()
    {
        if (rigged)
        {
            return monsterIdx;
        }
        monsterIdx = Random.Range(1, 4);
        return monsterIdx;
    }
}
