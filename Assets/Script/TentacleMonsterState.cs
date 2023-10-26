using System.Collections;
using UnityEngine;

[System.SerializableAttribute]
public class TentacleMonsterState : MonsterBaseState
{
    [SerializeField] GameObject tentacleMonster;
    public Animator anim;
    public float attackRate;
    public float swingTime = 0;
    public float attackTimer;
    public float animTimer;
    public float attackRateIncrease = 3f;
    public float damagePerAttack;
    public float wardOffRate = 50;
    public float rotationLowEnd, rotationHighEnd;
    public bool isBeingWardedOff;

    public override void EnterState(MonsterStateManager _monsterStateManager)
    {
        animTimer = Random.Range(5, 7);
        attackTimer = animTimer + 2.3f; 
        attackRate = 100;
        TriggerAttackSequence();
    }

    public override void UpdateState(MonsterStateManager _monsterStateManager)
    {
        AttackSequenceTimer();
        if (attackRate <= 0)
        {
            anim.SetTrigger("Slitherout");
            _monsterStateManager.SwitchStates(_monsterStateManager.idleState);
        }
    }

    void TriggerAttackSequence()
    {
        anim.SetTrigger("Idle");
        swingTime = Random.Range(0, 60);
        AudioManager.instance.PlaySFX(eSFX.creatureApproach, 0.55f);
    }

    void AttackSequenceTimer()
    {
        TakeDamageAnim();
        TakeDamage();
    }
    public void TakeDamageAnim()
    {
        animTimer -= Time.deltaTime;
        if (animTimer >= 0)
        {
            return;
        }
        anim.SetTrigger("Slapped");
        anim.SetTrigger("Idle");
        animTimer = Random.Range(5, 7);
    }
    private void TakeDamage()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer >= 0)
        {
            return;
        }
        attackTimer = animTimer + 2.3f;
        CameraShake.StartScreenShake(0.001f, 1);
        AudioManager.instance.PlaySFX(eSFX.creatureAttack, 1);
        ProgressionManager.AlterPlayerCourse((GetRandomOffset()));
        SubHealthManager.instance.TakeDamage(damagePerAttack);
    }

    float GetRandomOffset()
    {
        float randomOffset1 = Random.Range(rotationLowEnd, rotationHighEnd);
        float randomOffset2 = Random.Range(rotationLowEnd, rotationHighEnd);
        float trueRandomOffset = Random.Range(randomOffset1, randomOffset2);
        return trueRandomOffset;
    }

    public override void OnDisable()
    {
        ShockPlungerBehavior.onShock -= IsShocked;
    }

    public override void OnEnable()
    {
        ShockPlungerBehavior.onShock += IsShocked;
    }

    public void IsShocked()
    {
        Debug.Log("Shocked");
        attackRate -= wardOffRate;
        anim.SetTrigger("Shocked");
        anim.SetTrigger("Idle");
    }

    
}
