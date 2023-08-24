using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public float attackRate = 0;
    public float attackRateIncrease = 3f;
    public int attackDamage = 25;
    public int submarineHealth = 100;
    public float shakeDuration = 3f;
    bool isAttacking;
    public GameObject main;
    public Animator anim;
    public AnimationCurve shakeStrengthSmoothness;
    //public AudioClip[] monsterAudioClips;
    [SerializeField] AudioManager audioManager;

    private void Start()
    {
        anim = GetComponent<Animator>();
        main = GameObject.Find("Virtual Camera");
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
        audioManager.PlaySFX(eSFX.creatureApproach, 0.3f);
        int _randLook = Random.Range(3, 7);
        int _randattack = Random.Range(5, 10);

        yield return new WaitForSeconds(_randLook);
        anim.SetTrigger("LookAround");
        while (isAttacking == true)
        {
            yield return new WaitForSeconds(_randattack);
            submarineHealth -= attackDamage;
            StartCoroutine(ScreenShake());

        }
        yield return null;
    }

    IEnumerator ScreenShake()
    {
        Debug.Log("Shaaaaaaake");
        audioManager.PlaySFX(eSFX.creatureAttack, 1);
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
}
