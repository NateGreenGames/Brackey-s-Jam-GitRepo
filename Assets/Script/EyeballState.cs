using System.Collections;
using UnityEngine;

[System.SerializableAttribute]
public class EyeballState : MonsterBaseState 
{    
    [SerializeField] GameObject eyeballMonster;
    public float attackRate;
    public float swingTime;
    float attackTimer;
    float sfxTimer = 6f;
    public float attackRateIncrease = 3f;
    public float damagePerAttack;
    public float wardOffRate = 6;
    public float rotationLowEnd, rotationHighEnd;
    public bool isBeingWardedOff;
    public Animator anim;
    private bool isAttacking;


    public override void OnEnable()
    {
        ButtonBehavior.buttonEvent += UpdateLightState;
        FuseBox.OnOverload += OnOverload;
    }

    public override void OnDisable()
    {
        ButtonBehavior.buttonEvent -= UpdateLightState;
        FuseBox.OnOverload -= OnOverload;
    }

    public override void EnterState(MonsterStateManager _monsterStateManager)
    {
        AudioManager.instance.StartCoroutine(AudioManager.instance.FadeIn(AudioManager.instance.musicSource2, 7));
        AudioManager.instance.PlayMusic(eMusic.gameplayMusicDanger);
        attackTimer = Random.Range(2.5f, 4);
        attackRate = 100;
        TriggerAttackSequence();
    }

    public override void UpdateState(MonsterStateManager _monsterStateManager)
    {
        AttackSequenceTimer();
        WardOffMonster();
        if (attackRate <= 0)
        {
            anim.SetTrigger("Close Eye");
            anim.ResetTrigger("Squint Eye");
            AudioManager.instance.StartCoroutine(AudioManager.instance.FadeOut(AudioManager.instance.musicSource2, 7));
            _monsterStateManager.SwitchStates(_monsterStateManager.idleState);
        }
    }

    public override void Enrage(MonsterStateManager _monsterStateManager)
    {
        damagePerAttack = 100;
    }
    void TriggerAttackSequence()
    {
        anim.SetTrigger("Open Eye");
        //swingTime = Random.Range(0, 60);
        AudioManager.instance.PlaySFX(eSFX.creatureApproach, 0.55f);
    }

    void AttackSequenceTimer()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer >= 0)
        {
            return;
        }
        TakeDamage();
        attackTimer = Random.Range(1, 4);
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
            anim.SetTrigger("LookAround");
            anim.ResetTrigger("Squint Eye");
            return;
        }
        else if (isBeingWardedOff)
        {
            anim.SetTrigger("Squint Eye");
            anim.ResetTrigger("LookAround");
        }
        attackRate -= wardOffRate * Time.deltaTime;
        SFXTimer();
    }

    private void TakeDamage()
    {
        CameraShake.StartScreenShake(0.001f, 1);
        AudioManager.instance.PlaySFX(eSFX.creatureAttack, 1);
        ProgressionManager.AlterPlayerCourse((GetRandomOffset()));

        SubHealthManager.instance.TakeDamage(damagePerAttack);
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
            anim.SetTrigger("LookAround");
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
        if(_Index == 1)
        {
            isBeingWardedOff = _state;
        }
    }

    private void OnOverload()
    {
        isBeingWardedOff = false;
    }
}
