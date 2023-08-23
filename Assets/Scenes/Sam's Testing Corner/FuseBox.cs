using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBox : MonoBehaviour
{
    public GameObject[] fuseBoxLights;
    public GameObject submarineLight;
    Renderer materialRenderer;
    public Material[] lightMaterial = new Material[3];
    [SerializeField] Lever lever;
    public List<GameObject> tests; 
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
        CheckForLights(tests.Count);
        Debug.Log(tests.Count);
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
                break;
        }
    }

    void TurnLightsOn(int _lightsAmount)
    {
        for (int i = 0; i < _lightsAmount; i++)
        {
            lightMaterial[i].EnableKeyword("_EMISSION");
        }
        if (_lightsAmount == 3)
        {
            submarineLight.SetActive(false);
            lever.anim.SetTrigger("Up");
            lever.isInteractable = true;
            return;
        }
        for (int i = _lightsAmount; i > _lightsAmount - 1; i--)
        {
            lightMaterial[i].DisableKeyword("_EMISSION");
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            tests.Add(new GameObject());
            CheckForLights(tests.Count);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            tests.RemoveAt(tests.Count - 1);
            CheckForLights(tests.Count);
        }
    }
}
