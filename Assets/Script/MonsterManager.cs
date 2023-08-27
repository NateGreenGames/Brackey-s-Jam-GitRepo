using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager mM;
    public float attackRate = 0;
    public float swingTime = 0;
    public float attackRateIncrease = 3f;
    public float attackDamage;
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
    //public AudioClip[] monsterAudioClips;
    //[SerializeField] AudioManager audioManager;

    private void Awake()
    {
        mM = this;
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
        main = GameObject.Find("Virtual Camera");
        subWindow.SetFloat("_Blend", Mathf.Lerp(uncrackedBlendValue, crackedBlendValue, Mathf.InverseLerp(100, 0, submarineHealth)));
        StartCoroutine(WaitForAttackSequence());
    }
    public IEnumerator WaitForAttackSequence()
    {
        while (attackRate <= 100)
        {
            yield return new WaitForEndOfFrame();
            attackRate += Time.deltaTime * attackRateIncrease;
            if (attackRate >= 100)
            {
                Debug.Log("Start Attack");
                isAttacking = true;
                StartCoroutine(StartAttackSequence());
            }
        }
        yield return null;
    }

    public IEnumerator StartAttackSequence()
    {
        if (isBeingWardedOff)
        {
            StartCoroutine(WardOffMonster());
        }
        anim.SetTrigger("Open Eye");
        swingTime = Random.Range(0, 60);
        //audioManager.PlaySFX(eSFX.creatureApproach, 0.3f);
        AudioManager.instance.PlaySFX(eSFX.creatureApproach, 0.55f);
        int _randLook = Random.Range(1, 4);
        yield return new WaitForSeconds(_randLook);
        anim.SetTrigger("LookAround");
        StartCoroutine(TakingDamage());
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
            GetRandomOffset();

            StartCoroutine(ScreenShake());
        }
        yield return null;
    }

    IEnumerator ScreenShake()
    {
        Debug.Log("Shaaaaaaake");
        //audioManager.PlaySFX(eSFX.creatureAttack, 1);
        AudioManager.instance.PlaySFX(eSFX.creatureAttack, 0.5f);
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

    public IEnumerator WardOffMonster()
    {
        if (!isAttacking)
        {
            Debug.Log("No no... monster is not hungry yet");
            yield return null;
        }
        else
        {
            while (isBeingWardedOff)
            {
                yield return new WaitForEndOfFrame();
                attackRate -= wardOffRate * Time.deltaTime;

                if (attackRate < swingTime)
                {
                    StopAllCoroutines();
                    anim.SetTrigger("Close Eye");
                    AudioManager.instance.PlaySFX(eSFX.creatureFlee, 0.4f);
                    StartCoroutine(WaitForAttackSequence());
                    isAttacking = false;
                    yield break;
                }
            }
            if (attackRate > swingTime)
            {
                yield break;
            }

            Debug.Log("Lmao");
            anim.SetTrigger("Close Eye");
            StartCoroutine(WaitForAttackSequence());
        }
    }

    IEnumerator TakingDamage()
    {
        while (isAttacking)
        {
            yield return new WaitForEndOfFrame();
            submarineHealth -= attackDamage * Time.deltaTime;
            ProgressionManager.AlterPlayerCourse((GetRandomOffset() * Time.deltaTime));
            if (submarineHealth <= 0)
            {
                isAttacking = false;
            }
            yield return null;
        }
        if (submarineHealth <= 0)
        {
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

    public void Enrage()
    {
        attackRate = 100;
        attackDamage = 100;
    }
}
