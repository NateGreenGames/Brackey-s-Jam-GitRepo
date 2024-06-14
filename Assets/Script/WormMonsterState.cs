using System.Collections;
using UnityEngine;

[System.SerializableAttribute]
public class WormMonsterState : MonsterBaseState
{
    [SerializeField] GameObject wormMonster;
    public Animator anim;
    public AudioSource audioSource;
    public float attackRate;
    float attackTimer = 2.3f;
    public float damagePerAttack;
    private float wardOffRate = 6;
    public float rotationLowEnd, rotationHighEnd;
    private bool isBeingWardedOff;


    private bool canAttack = true;
    public override void EnterState(MonsterStateManager _monsterStateManager)
    {
        audioSource.Play();
        wormMonster.transform.position = new Vector3(0, 1.355f, 1.41f);
        AudioManager.instance.StartCoroutine(AudioManager.instance.FadeIn(AudioManager.instance.musicSource2, 7));
        AudioManager.instance.PlaySFX(eSFX.wormIntro, 0.55f);
        AudioManager.instance.PlayMusic(eMusic.gameplayMusicDanger);
        attackRate = 100;
        anim.SetTrigger("Attatch");
    }

    public override void OnDisable()
    {
        AirCannonValve.OnAirCannonTick -= AirCannonDamage;
    }

    public override void OnEnable()
    {
        AirCannonValve.OnAirCannonTick += AirCannonDamage;
    }

    public override void UpdateState(MonsterStateManager _monsterStateManager)
    {
        AttackSequenceTimer();
        WardOffMonster();
        if (attackRate <= 0)
        {
            audioSource.Stop();
            anim.SetTrigger("Detach");
            anim.ResetTrigger("Idle");
            anim.ResetTrigger("Hurt");
            AudioManager.instance.StartCoroutine(AudioManager.instance.FadeOut(AudioManager.instance.musicSource2, 7));
            SteamManager.instance.UnlockAchievement(eAchievement.SurviveWorm);
            _monsterStateManager.SwitchStates(_monsterStateManager.idleState);
        }
    }
    public override void Enrage(MonsterStateManager _monsterStateManager)
    {
        attackRate = 100;
        damagePerAttack = 100;
    }

    void AttackSequenceTimer()
    {
        attackTimer -= Time.deltaTime;
        if(attackTimer > 0)
        {
            return;
        }
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        TakeDamage();
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
            attackRate -= wardOffRate;
        }
    }

    private void TakeDamage()
    {
        if (canAttack)
        {
            attackTimer = 2.3f;
            CameraShake.StartScreenShake(0.0005f, attackTimer);
            ProgressionManager.AlterPlayerCourse(GetRandomOffset());
            SubHealthManager.instance.TakeDamage(damagePerAttack);

            if (isBeingWardedOff)
            {
                return;
            }
            else
            {
                anim.SetTrigger("Idle");
            }
        }

        if(SubHealthManager.submarineHealth <= 0 && canAttack == true)
        {
            canAttack = false;
            AudioManager.instance.StartCoroutine(AudioManager.instance.FadeOut(AudioManager.instance.musicSource2, 7));
            audioSource.Stop();
            anim.SetTrigger("Idle");

            if(ElectricityUI.electricityPercentage > 0)
            {
                SteamManager.instance.UnlockAchievement(eAchievement.DieToMonster);
            }
        }

        if (GameOverAndCompletionController.instance.isOver)
        {
            canAttack = false;
            AudioManager.instance.StartCoroutine(AudioManager.instance.FadeOut(AudioManager.instance.musicSource2, 7));
            audioSource.Stop();
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

    private void AirCannonDamage(float _dmg)
    {
        wardOffRate = _dmg;
        if (_dmg == 0)
        {
            isBeingWardedOff = false;
        }
        else
        {
            isBeingWardedOff = true;
        }
    }
}
