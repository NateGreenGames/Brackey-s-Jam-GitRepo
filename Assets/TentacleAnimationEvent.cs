using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleAnimationEvent : MonoBehaviour
{
    [SerializeField] MonsterStateManager stateManager;



    public void CallTakeDamage()
    {
        stateManager.tentacleMonsterState.TakeDamage();
    }
}
