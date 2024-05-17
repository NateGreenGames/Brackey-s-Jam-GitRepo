using System.Collections;
using UnityEngine;

[System.SerializableAttribute]
public class EyeballState : MonsterBaseState 
{    
    [SerializeField] GameObject eyeballMonster;
    public float attackRate;
    float attackTimer;
    public float damagePerAttack;
    public float wardOffRate = 6;
    public float rotationLowEnd, rotationHighEnd;
    public bool isBeingWardedOff;
    public Animator anim;

    private bool canAttack = true;
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
        AudioManager.instance.PlaySFX(eSFX.eyeIntro, 0.55f);
        attackTimer = Random.Range(2.5f, 4);
        attackRate = 100;
        anim.SetTrigger("Open Eye");
    }

    public override void UpdateState(MonsterStateManager _monsterStateManager)
    {
        AttackSequenceTimer();
        WardOffMonster();
        if (attackRate <= 0)
        {
            anim.SetTrigger("Close Eye");
            anim.ResetTrigger("Squint Eye");
            AudioManager.instance.PlaySFX(eSFX.creatureFlee, 0.4f);
            AudioManager.instance.StartCoroutine(AudioManager.instance.FadeOut(AudioManager.instance.musicSource2, 7));
            _monsterStateManager.SwitchStates(_monsterStateManager.idleState);
        }
    }

    public override void Enrage(MonsterStateManager _monsterStateManager)
    {
        damagePerAttack = 100;
    }

    void AttackSequenceTimer()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer > 0)
        {
            return;
        }
        TakeDamage();
        attackTimer = Random.Range(2.5f, 4f);
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
    }

    private void TakeDamage()
    {
        if (canAttack)
        {
            CameraShake.StartScreenShake(0.001f, 1);
            AudioManager.instance.PlaySFX(eSFX.creatureAttack, 1);
            ProgressionManager.AlterPlayerCourse((GetRandomOffset()));

            SubHealthManager.instance.TakeDamage(damagePerAttack);


            if (isBeingWardedOff)
            {
                return;
            }
            else
            {
                anim.SetTrigger("LookAround");
            }
        }

        if (SubHealthManager.submarineHealth <= 0 && canAttack == true)
        {
            canAttack = false;
            AudioManager.instance.StartCoroutine(AudioManager.instance.FadeOut(AudioManager.instance.musicSource2, 7));
            anim.SetTrigger("LookAround");
        }

        if (GameOverAndCompletionController.instance.isOver)
        {
            canAttack = false;
            AudioManager.instance.StartCoroutine(AudioManager.instance.FadeOut(AudioManager.instance.musicSource2, 7));
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
