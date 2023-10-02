using System.Collections;
using UnityEngine;

[System.SerializableAttribute]
public class EyeballState : MonsterBaseState 
{    
    [SerializeField] GameObject eyeballMonster;
    public float attackRate;
    public float swingTime = 0;
    float attackTimer;
    float sfxTimer = 6f;
    public float attackRateIncrease = 3f;
    public float damagePerAttack;
    public float wardOffRate = 6;
    public float submarineHealth = 100;
    public float rotationLowEnd, rotationHighEnd;
    public bool isBeingWardedOff;
    public Animator anim;
    private bool isAttacking;

    //Sub Health Stuff
    public Material subWindow;
    public float uncrackedBlendValue, crackedBlendValue;


    public override void OnEnable()
    {
        ButtonBehavior.buttonEvent += UpdateLightState;
    }

    public override void OnDisable()
    {
        ButtonBehavior.buttonEvent -= UpdateLightState;
    }

    public override void EnterState(MonsterStateManager _monsterStateManager)
    {
        if (eyeballMonster == null)
        {
            Debug.Log("Obtaining references for Eyeball monster");
            eyeballMonster = GameObject.Find("TP_Eyeball 1 Variant");
            anim = eyeballMonster.GetComponent<Animator>();
            subWindow.SetFloat("_Blend", Mathf.Lerp(uncrackedBlendValue, crackedBlendValue, Mathf.InverseLerp(100, 0, submarineHealth)));
        }
        attackTimer = Random.Range(1, 4);
        attackRate = 100;
        TriggerAttackSequence();
    }

    public override void UpdateState(MonsterStateManager _monsterStateManager)
    {
        AttackSequenceTimer();
        WardOffMonster();
        if (attackRate <= swingTime)
        {
            anim.SetTrigger("Close Eye");
            anim.ResetTrigger("Squint Eye");
            _monsterStateManager.SwitchStates(_monsterStateManager.idleState);
        }
    }

    void TriggerAttackSequence()
    {
        anim.SetTrigger("Open Eye");
        swingTime = Random.Range(0, 60);
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

    private void TakeDamage()   //MOVE this function to update and add a condition + timer. 
    {
        //anim.SetTrigger("LookAround");
        submarineHealth -= damagePerAttack;
        Debug.Log(submarineHealth);
        //Call screen shake routine on camera.
        AudioManager.instance.PlaySFX(eSFX.creatureAttack, 1);
        ProgressionManager.AlterPlayerCourse((GetRandomOffset() * Time.deltaTime));

        if (submarineHealth <= 0)
        {
            isAttacking = false;
            AudioManager.instance.PlaySFX(eSFX.crush, 1f);
            GameOverAndCompletionController.instance.EndGame("You were crushed and swallowed whole.");
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
}
