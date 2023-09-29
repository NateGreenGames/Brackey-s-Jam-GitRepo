using System.Collections;
using UnityEngine;

public class EyeballState : MonsterBaseState
{
    [SerializeField] GameObject eyeballMonster;
    public float attackRate = 0;
    public float swingTime = 0;
    public float attackRateIncrease = 3f;
    public float damagePerAttack;
    public float wardOffRate = 6;
    public float submarineHealth = 100;
    public float shakeDuration = 3f;
    public float rotationLowEnd, rotationHighEnd;
    bool isAttacking;
    public bool isBeingWardedOff;
    public GameObject main;
    public Animator anim;
    public AnimationCurve shakeStrengthSmoothness;
    public Material subWindow;
    public float uncrackedBlendValue, crackedBlendValue;

    public override void EnterState(MonsterStateManager _monsterStateManager)
    {
        if (eyeballMonster == null)
        {
            Debug.Log("Obtaining references for Eyeball monster");
            eyeballMonster = GameObject.Find("TP_Eyeball 1 Variant");
            anim = eyeballMonster.GetComponent<Animator>();
            main = GameObject.Find("Virtual Camera");
            subWindow.SetFloat("_Blend", Mathf.Lerp(uncrackedBlendValue, crackedBlendValue, Mathf.InverseLerp(100, 0, submarineHealth)));
        }
    }

    public override void UpdateState(MonsterStateManager _monsterStateManager)
    {
        
    }

    /*private void Start()
    {
        //anim = GetComponent<Animator>();
        main = GameObject.Find("Virtual Camera");
        subWindow.SetFloat("_Blend", Mathf.Lerp(uncrackedBlendValue, crackedBlendValue, Mathf.InverseLerp(100, 0, submarineHealth)));
        //StartCoroutine(WaitForAttackSequence());
    }*/


    public IEnumerator StartAttackSequence()
    {
        if (isBeingWardedOff)
        {
            //StartCoroutine(WardOffMonster());
        }
        anim.SetTrigger("Open Eye");
        swingTime = Random.Range(0, 60);
        //audioManager.PlaySFX(eSFX.creatureApproach, 0.3f);
        AudioManager.instance.PlaySFX(eSFX.creatureApproach, 0.55f);
        int _randLook = Random.Range(1, 4);
        yield return new WaitForSeconds(_randLook);
        if (!isBeingWardedOff)
        {
            anim.SetTrigger("LookAround");
        }
        //StartCoroutine(TakingDamage());
        while (isAttacking == true)
        {
            float _randattack = Random.Range(1.5f, 4f);
            yield return new WaitForSeconds(_randattack);
            //Update window texture blending.
            subWindow.SetFloat("_Blend", Mathf.Lerp(uncrackedBlendValue, crackedBlendValue, Mathf.InverseLerp(100, 0, submarineHealth)));
            if (submarineHealth <= 0)
            {
                Debug.Log("You died...");
            }
            
        }
        yield return null;
    }

    public void WardOffMonster()
    {
            if (isBeingWardedOff)
            {
                anim.SetTrigger("Squint Eye");
            }
            while (isBeingWardedOff)
            {
                attackRate -= wardOffRate * Time.deltaTime;

                if (attackRate < swingTime)
                {
                    //StopAllCoroutines();
                    anim.SetTrigger("Close Eye");
                    AudioManager.instance.PlaySFX(eSFX.creatureFlee, 0.4f);
                    isAttacking = false;
                }
            }
            if (attackRate > swingTime)
            {
                anim.SetTrigger("LookAround");
            }

            Debug.Log("Lmao");
            anim.SetTrigger("Close Eye");
    }

    private void TakeDamage()
    {
        submarineHealth -= damagePerAttack;
        //Call screen shake routine on camera.
        AudioManager.instance.PlaySFX(eSFX.creatureAttack, 1);
        ProgressionManager.AlterPlayerCourse((GetRandomOffset() * Time.deltaTime));

        if (submarineHealth <= 0)
        {
            isAttacking = false;
            AudioManager.instance.PlaySFX(eSFX.crush, 1f);
            GameOverAndCompletionController.instance.EndGame("You were crushed and swallowed whole.");
        }
    }

    float GetRandomOffset()
    {
        float randomOffset1 = Random.Range(rotationLowEnd, rotationHighEnd);
        float randomOffset2 = Random.Range(rotationLowEnd, rotationHighEnd);
        float trueRandomOffset = Random.Range(randomOffset1, randomOffset2);
        return trueRandomOffset;
    }

    //This needs moved to a new component on the main camera. The creature doesn't need to know this logic.
    IEnumerator ScreenShake()
    {
        Vector3 startPos = main.transform.position;
        float timeElapsed = 0f;

        while (timeElapsed < shakeDuration)
        {
            timeElapsed += Time.deltaTime;
            float shakeStrength = shakeStrengthSmoothness.Evaluate(timeElapsed / shakeDuration);
            main.transform.position = startPos + Random.insideUnitSphere * shakeStrength;
            yield return null;
        }

        main.transform.position = startPos;
    }

    //This should probably be moved up to the manager level so it can affect whatever creature is currently active.
    public void Enrage()
    {
        attackRate = 100;
        damagePerAttack = 100;
    }
    
}
