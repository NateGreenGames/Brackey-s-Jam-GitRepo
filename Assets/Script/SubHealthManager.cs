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
    }


    public void TakeDamage(float damageToTake)
    {
        submarineHealth -= damageToTake;
        subWindow.SetFloat("_Blend", Mathf.Lerp(uncrackedBlendValue, crackedBlendValue, Mathf.InverseLerp(100, 0, submarineHealth)));
        if (submarineHealth <= 0)
        {
            AudioManager.instance.PlaySFX(eSFX.crush, 1f);
            GameOverAndCompletionController.instance.EndGame("You were crushed and swallowed whole.");
        }
    }
}
