using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager mM;
    public float attackRate = 0;
    public float swingTime = 75;
    public float attackRateIncrease = 3f;
    public int attackDamage = 25;
    public float wardOffRate = 6;
    public int submarineHealth = 100;
    public float shakeDuration = 3f;
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
        anim.SetTrigger("Open Eye");
        //audioManager.PlaySFX(eSFX.creatureApproach, 0.3f);
        AudioManager.instance.PlaySFX(eSFX.creatureApproach, 0.45f);
        int _randLook = Random.Range(3, 7);
        int _randattack = Random.Range(5, 10);

        yield return new WaitForSeconds(_randLook);
        anim.SetTrigger("LookAround");
        while (isAttacking == true)
        {
            yield return new WaitForSeconds(_randattack);
            submarineHealth -= attackDamage;
            //Update window texture blending.
            subWindow.SetFloat("_Blend", Mathf.Lerp(uncrackedBlendValue, crackedBlendValue, Mathf.InverseLerp(100, 0, submarineHealth)));
            if (submarineHealth <= 0)
            {
                Debug.Log("You died...");
            }
            //ProgressionManager.AlterPlayerCourse(Random.Range(-10f * Time.deltaTime, 10f * Time.deltaTime));
            StartCoroutine(ScreenShake());
        }
        yield return null;
    }

    IEnumerator ScreenShake()
    {
        Debug.Log("Shaaaaaaake");
        //audioManager.PlaySFX(eSFX.creatureAttack, 1);
        AudioManager.instance.PlaySFX(eSFX.creatureAttack, 1);
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
                if (attackRate <= 0)
                {
                    anim.SetTrigger("Close Eye");
                    StartCoroutine(WaitForAttackSequence());
                    attackRate = 0;
                    yield break;
                }
                if (attackRate < swingTime)
                {
                    StopAllCoroutines();
                    isAttacking = false;
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
    }
