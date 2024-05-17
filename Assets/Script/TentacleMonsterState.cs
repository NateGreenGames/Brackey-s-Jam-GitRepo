using System.Collections;
using UnityEngine;

[System.SerializableAttribute]
public class TentacleMonsterState : MonsterBaseState
{
    [SerializeField] GameObject tentacleMonster;
    public Animator anim;
    public float attackRate;
    public float shockedTimer = 6.5f;
    public float animTimer;
    public float attackRateIncrease = 3f;
    public float damagePerAttack;
    public float rotationLowEnd, rotationHighEnd;
    public bool isBeingWardedOff;
    public bool isShocked;


    private bool canAttack = true;
    [SerializeField] ParticleSystem[] m_PS;

    private bool inSlapLoop = false;

    public override void EnterState(MonsterStateManager _monsterStateManager)
    {
        anim.SetTrigger("Slitherin");
        AudioManager.instance.StartCoroutine(AudioManager.instance.FadeIn(AudioManager.instance.musicSource2, 7));
        AudioManager.instance.PlayMusic(eMusic.gameplayMusicDanger);
        AudioManager.instance.PlaySFX(eSFX.tentacleIntro, 0.55f);
        animTimer = 5.5f;
        inSlapLoop = false;
        attackRate = 100f;
    }

    public override void UpdateState(MonsterStateManager _monsterStateManager)
    {
        if (isShocked)
        {
            shockedTimer -= Time.deltaTime;
            if (shockedTimer >= 0)
            {
                return;
            }
            foreach (ParticleSystem system in m_PS)
            {
                system.Clear();
                system.Stop();
            }
            isShocked = false;
            anim.SetTrigger("Idle");
            shockedTimer = 6.5f;
        }
        else
        {
            AttackSequenceTimer();
        }


        if (attackRate <= 0)
        {
            anim.SetTrigger("Slitherout");
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
        animTimer -= Time.deltaTime;
        if(animTimer >= 0)
        {
            return;
        }
        else if(!inSlapLoop)
        {
            anim.SetTrigger("Idle");
            anim.SetTrigger("Slapped");
            inSlapLoop = true;
        }
    }

    public void TakeDamage()
    {
        if (canAttack)
        {
            CameraShake.StartScreenShake(0.001f, 1);
            AudioManager.instance.PlaySFX(eSFX.creatureAttack, 1);
            ProgressionManager.AlterPlayerCourse((GetRandomOffset()));
            SubHealthManager.instance.TakeDamage(damagePerAttack);
        }

        if (SubHealthManager.submarineHealth <= 0 && canAttack == true)
        {
            canAttack = false;
            AudioManager.instance.StartCoroutine(AudioManager.instance.FadeOut(AudioManager.instance.musicSource2, 7));
            anim.ResetTrigger("Slapped");
            anim.ResetTrigger("Idle");
            anim.SetTrigger("Idle");
        }

        if (GameOverAndCompletionController.instance.isOver)
        {
            canAttack = false;
            AudioManager.instance.StartCoroutine(AudioManager.instance.FadeOut(AudioManager.instance.musicSource2, 7));
            anim.ResetTrigger("Slapped");
            anim.ResetTrigger("Idle");
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
        AudioManager.instance.PlaySFX(eSFX.tentacleOutro, 2);
        foreach (ParticleSystem system in m_PS)
        {
            system.Play();
        }
        isShocked = true;
        attackRate = 0;
        anim.SetTrigger("Shocked");
        anim.ResetTrigger("Slapped");
        anim.SetTrigger("Idle");
    }
}
