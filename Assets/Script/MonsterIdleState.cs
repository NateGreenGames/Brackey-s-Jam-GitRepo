using System.Collections;
using UnityEngine;

public class MonsterIdleState : MonsterBaseState
{
    public float attackRate = 0;
    public float attackRateIncrease = 3f;
    public override void EnterState(MonsterStateManager _monsterStateManager)
    {

    }

    public override void UpdateState(MonsterStateManager _monsterStateManager)
    {
    }
    public IEnumerator WaitForAttackSequence()
    {
        while (attackRate <= 100)
        {
            yield return new WaitForEndOfFrame();
            attackRate += Time.deltaTime * attackRateIncrease;
            if (attackRate >= 100)
            {
                Debug.Log("Start Attack");
                //isAttacking = true; REMEMBER to set isAttacking to true on EyeballState script
                //StartCoroutine(StartAttackSequence());
            }
        }
        yield return null;
    }
}
