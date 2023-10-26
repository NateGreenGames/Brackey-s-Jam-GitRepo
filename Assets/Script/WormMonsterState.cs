using System.Collections;
using UnityEngine;

[System.SerializableAttribute]
public class WormMonsterState : MonsterBaseState
{
    [SerializeField] GameObject wormMonster;
    public Animator anim;
    public float attackRate;
    public float swingTime = 0;
    float attackTimer;
    float sfxTimer = 6f;
    public float attackRateIncrease = 3f;
    public float damagePerAttack;
    public float wardOffRate = 6;
    public float rotationLowEnd, rotationHighEnd;
    public bool isBeingWardedOff;

    public override void EnterState(MonsterStateManager _monsterStateManager)
    {
        throw new System.NotImplementedException();
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
        throw new System.NotImplementedException();
    }
}
