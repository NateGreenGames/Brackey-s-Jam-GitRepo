using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsSceneBehavior : MonoBehaviour
{
    public float creditsDuration;
    public Animator creditsAnimator;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreditsRoutine());
    }

    
    private IEnumerator CreditsRoutine()
    {
        creditsAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(creditsDuration);
        LoadingManager.instance.ChangeScene("MainMenu", 3, 3);
    }
}
