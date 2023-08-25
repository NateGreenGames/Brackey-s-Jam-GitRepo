using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBox : MonoBehaviour
{
    public GameObject[] fuseBoxLights;
    Renderer materialRenderer;
    public Material[] lightMaterial = new Material[3];
    public static FuseBox fB;
    [SerializeField] Lever lever;
    public bool isOverloaded = false;
    //public List<GameObject> tests; 
    private void Awake()
    {
        fB = this;
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
        Debug.Log(ElectricityManager.ActiveUsers.Count);
    }

    public void CheckForLights(int _lightsAmount)
    {
        if (_lightsAmount >= 4)
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

    }

    public void Overload()
    {
        for (int i = ElectricityManager.ActiveUsers.Count - 1; i >= 0; i--)
        {
            ElectricityManager.ActiveUsers[i].ToggleActiveState();
            lever.anim.SetTrigger("LeverUp");
            lever.isInteractable = true;
            isOverloaded = true;
        }
    }
}
