using System.Collections;
using UnityEngine;

[System.SerializableAttribute]
public class WormMonsterState : MonsterBaseState
{
    [SerializeField] GameObject wormMonster;
    public Animator anim;
    public AudioSource audioSource;
    public float attackRate;
    float attackTimer;
    float sfxTimer = 2.3f;
    public float attackRateIncrease = 3f;
    public float damagePerAttack;
    public float wardOffRate = 6;
    public float rotationLowEnd, rotationHighEnd;
    public bool isBeingWardedOff;

    public override void EnterState(MonsterStateManager _monsterStateManager)
    {
        wormMonster.transform.position = new Vector3(0, 1.355f, 1.41f);
        AudioManager.instance.StartCoroutine(AudioManager.instance.FadeIn(AudioManager.instance.musicSource2, 7));
        AudioManager.instance.PlayMusic(eMusic.gameplayMusicDanger);
        attackTimer = Random.Range(2.5f, 4);
        attackRate = 100;
        TriggerAttackSequence();
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
            _monsterStateManager.SwitchStates(_monsterStateManager.idleState);
        }
    }
    public override void Enrage(MonsterStateManager _monsterStateManager)
    {
        damagePerAttack = 100;
    }
    void TriggerAttackSequence()
    {
        anim.SetTrigger("Attatch");
        AudioManager.instance.PlaySFX(eSFX.creatureApproach, 0.55f);
    }

    void AttackSequenceTimer()
    {
        sfxTimer -= Time.deltaTime;
        if (sfxTimer >= 0)
        {
            return;
        }
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
        //SFXTimer();
    }

    private void TakeDamage()
    {
        //CameraShake.StartScreenShake(0.001f, 1);
        //AudioManager.instance.PlaySFX(eSFX.creatureAttack, 1);
        ProgressionManager.AlterPlayerCourse((GetRandomOffset() * Time.deltaTime));
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        SubHealthManager.instance.TakeDamage(damagePerAttack * Time.deltaTime);

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

    private void AirCannonDamage(float _dmg)
    {
        attackRate -= _dmg;
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
