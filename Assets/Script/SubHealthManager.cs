using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubHealthManager : MonoBehaviour
{
    public static SubHealthManager instance;
    //Sub Health Stuff
    public static float submarineHealth = 100;

    private Material subWindow;
    private float uncrackedBlendValue = 1;
    private float crackedBlendValue = -1;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        subWindow = gameObject.GetComponent<MeshRenderer>().material;
        submarineHealth = 100f;
    }


    public void TakeDamage(float damageToTake)
    {
        submarineHealth -= damageToTake;
        subWindow.SetFloat("_Blend", Mathf.Lerp(uncrackedBlendValue, crackedBlendValue, Mathf.InverseLerp(100, 0, submarineHealth)));
        if (submarineHealth <= 0)
        {
            AudioManager.instance.PlaySFX(eSFX.crush, 1f);
            switch (SubtitleController.language)
            {
                case eSubtitleLanguage.English:
                    GameOverAndCompletionController.instance.EndGame("You were crushed and swallowed whole.");
                    break;
                case eSubtitleLanguage.Spanish:
                    GameOverAndCompletionController.instance.EndGame("Fuiste aplastado y tragado entero.");
                    break;
            }
        }
    }
}
