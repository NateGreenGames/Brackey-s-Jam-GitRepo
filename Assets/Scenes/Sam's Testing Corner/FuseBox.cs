using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBox : MonoBehaviour
{
    public delegate void blankEvent();
    public static event blankEvent OnOverload;

    public GameObject[] fuseBoxLights;
    public Material[] lightMaterial = new Material[3];
    public GameObject[] objectsToHideDuringOverload;
    public static FuseBox instance;
    [SerializeField] Lever lever;
    public bool isOverloaded = false;
    public Light overloadLight;
    public int valueToOverload;

    public bool postDebugInfo;
    public MonsterStateManager monsterManager;
    //public List<GameObject> tests; 
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if (!lever)
        {
            GameObject _lever = GameObject.Find("Handle");
            if (_lever != null)
            {
                lever = _lever.GetComponent<Lever>();
            }
        }
        for (int i = 0; i < fuseBoxLights.Length; i++)
        {
            lightMaterial[i] = fuseBoxLights[i].GetComponent<Renderer>().material;
        }
    }

    private void Update()
    {
        CheckForLights(ElectricityManager.ActiveUsers.Count);
        if(postDebugInfo) Debug.Log(ElectricityManager.ActiveUsers.Count);
    }

    public void CheckForLights(int _lightsAmount)
    {
        if (isOverloaded)
        {
            overloadLight.enabled = true;
        }
        else
        {
            overloadLight.enabled = false;
        }
        if (_lightsAmount >= valueToOverload)
        {
            Overload();
        }

        for (int i = 0; i < lightMaterial.Length; i++)
        {
            if(i < _lightsAmount)
            {
                lightMaterial[i].EnableKeyword("_EMISSION");
            }
            else
            {
                lightMaterial[i].DisableKeyword("_EMISSION");
            }
        }

        for (int i = 0; i < objectsToHideDuringOverload.Length; i++)
        {
            if (!isOverloaded && objectsToHideDuringOverload[i].activeInHierarchy == false)
            {
                objectsToHideDuringOverload[i].SetActive(true);
            }
            else if(isOverloaded && objectsToHideDuringOverload[i].activeInHierarchy == true)
            {
                objectsToHideDuringOverload[i].SetActive(false);
            }
        }
    }

    public void Overload()
    {
        lever.anim.SetTrigger("LeverDown");
        AudioManager.instance.PlaySFX(eSFX.powerOff, 0.5f);
        AudioManager.instance.PlaySFX(eSFX.leverPushPull, 0.2f);
        isOverloaded = true;

        for (int i = ElectricityManager.ActiveUsers.Count - 1; i >= 0; i--)
        {
            ElectricityManager.ActiveUsers[i].OnOverload();
        }
        OnOverload?.Invoke();
    }

    public void RunOutOfPower()
    {
        Overload();
        monsterManager.Enrage();

    }
}
