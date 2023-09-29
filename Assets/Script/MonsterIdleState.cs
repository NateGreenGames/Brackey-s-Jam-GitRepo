using System.Collections;
using UnityEngine;

[System.SerializableAttribute]
public class MonsterIdleState : MonsterBaseState
{
    public float attackRate = 0;
    public float attackRateIncrease = 3f;
    public override void EnterState(MonsterStateManager _monsterStateManager)
    {
        Debug.Log("IdleState");       
    }

    public override void UpdateState(MonsterStateManager _monsterStateManager)
    {
        WaitForAttackSequence();
        if (attackRate >= 100)
        {
            attackRate = 0;
            _monsterStateManager.SwitchStates(_monsterStateManager.eyeballState);
        }
    }
    public void WaitForAttackSequence()
    {
        attackRate += Time.deltaTime * attackRateIncrease;
        Debug.Log(attackRate);
        if (attackRate >= 100)
        {
            Debug.Log("Start Attack");

        }
    }
}
