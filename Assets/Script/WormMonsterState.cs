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
    bool isAttacking;

    public override void EnterState(MonsterStateManager _monsterStateManager)
    {
        AudioManager.instance.StartCoroutine(AudioManager.instance.FadeIn(AudioManager.instance.musicSource2, 7));
        AudioManager.instance.PlayMusic(eMusic.gameplayMusicDanger);
        attackTimer = Random.Range(2.5f, 4);
        attackRate = 100;
        TriggerAttackSequence();
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
        AttackSequenceTimer();
        WardOffMonster();
        if (attackRate <= swingTime)
        {
            anim.SetTrigger("Detach");
            anim.ResetTrigger("Idle");
            AudioManager.instance.StartCoroutine(AudioManager.instance.FadeOut(AudioManager.instance.musicSource2, 7));
            _monsterStateManager.SwitchStates(_monsterStateManager.idleState);
        }
    }

    void TriggerAttackSequence()
    {
        anim.SetTrigger("Attatch");
        swingTime = Random.Range(0, 60);
        AudioManager.instance.PlaySFX(eSFX.creatureApproach, 0.55f);
    }

    void AttackSequenceTimer()
    {       
        TakeDamage();
    }

    void SFXTimer()
    {
        sfxTimer -= Time.deltaTime;
        if (sfxTimer >= 0)
        {
            return;
        }
        AudioManager.instance.PlaySFX(eSFX.creatureFlee, 0.4f);
        sfxTimer = 6;
    }

    public void WardOffMonster()
    {
        if (!isBeingWardedOff)
        {
            anim.SetTrigger("Idle");
            anim.ResetTrigger("Hurt");
            return;
        }
        else if (isBeingWardedOff)
        {
            anim.SetTrigger("Hurt");
            anim.ResetTrigger("Idle");
        }
        attackRate -= wardOffRate * Time.deltaTime;
        SFXTimer();
    }

    private void TakeDamage()
    {
        //CameraShake.StartScreenShake(0.001f, 1);
        //AudioManager.instance.PlaySFX(eSFX.creatureAttack, 1);
        ProgressionManager.AlterPlayerCourse((GetRandomOffset() * Time.deltaTime));

        SubHealthManager.instance.TakeDamage(damagePerAttack * Time.deltaTime);
        if (SubHealthManager.instance.submarineHealth <= 0)
        {
            isAttacking = false;
        }

        if (isBeingWardedOff)
        {
            return;
        }
        else
        {
            anim.SetTrigger("Idle");
        }
    }

    float GetRandomOffset()
    {
        float randomOffset1 = Random.Range(rotationLowEnd, rotationHighEnd);
        float randomOffset2 = Random.Range(rotationLowEnd, rotationHighEnd);
        float trueRandomOffset = Random.Range(randomOffset1, randomOffset2);
        return trueRandomOffset;
    }

    private void UpdateLightState(int _Index, bool _state)
    {
        if (_Index == 1)
        {
            isBeingWardedOff = _state;
        }
    }
}
