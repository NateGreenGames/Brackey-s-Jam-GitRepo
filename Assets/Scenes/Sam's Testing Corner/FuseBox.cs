using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBox : MonoBehaviour
{
    public GameObject[] fuseBoxLights;
    Renderer materialRenderer;
    public Material[] lightMaterial = new Material[3];
    [SerializeField] Lever lever;
    //public List<GameObject> tests; 
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
        switch (_lightsAmount)
        {
            case 0:
                TurnLightsOn(_lightsAmount);
                break;
            case 1:
                TurnLightsOn(_lightsAmount);
                break;
            case 2:
                TurnLightsOn(_lightsAmount);
                break;
            case 3:
                TurnLightsOn(_lightsAmount);
                break;
            default:
                Overload();
                break;
        }
    }

    void TurnLightsOn(int _lightsAmount)
    {
        for (int i = 0; i < _lightsAmount; i++)
        {
            Debug.Log("Looping " + _lightsAmount);
            lightMaterial[i].EnableKeyword("_EMISSION");
        }
        /*if (_lightsAmount == 3)
        {
            lever.anim.SetTrigger("Up");
            lever.isInteractable = true;
            return;
        }*/
        for (int i = _lightsAmount; i > _lightsAmount - 1; i--)
        {
            lightMaterial[i].DisableKeyword("_EMISSION");
        }
    }

    public void Overload()
    {
        for (int i = 0; i < ElectricityManager.ActiveUsers.Count; i++)
        {
            ElectricityManager.ActiveUsers[i].ToggleActiveState();
            lever.anim.SetTrigger("Up");
            lever.isInteractable = true;
            return;
        }
    }
}
