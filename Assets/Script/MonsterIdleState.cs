using System.Collections;
using UnityEngine;

[System.Serializable]
public class MonsterIdleState : MonsterBaseState
{
    public float attackRate = 0;
    public float attackRateIncrease = 3f;

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
            _monsterStateManager.SwitchStates(_monsterStateManager.eyeballState);
        }
    }
    public void WaitForAttackSequence()
    {
        attackRate += Time.deltaTime * attackRateIncrease;
        Debug.Log(attackRate);
    }
}
