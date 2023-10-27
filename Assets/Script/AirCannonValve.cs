using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCannonValve : MonoBehaviour, IInteractable
{
    public delegate void Blank(float _power);
    public static event Blank OnAirCannonTick;

    [SerializeField][Range(0, 5)] private int stage;
    [SerializeField] private float airCannonPower;

    private Animator m_anim;
    public bool isInteractable { get; set; }

    public void OnInteract()
    {
        StartCoroutine(HoldingDownLever());
    }

    public void OnInteractEnd()
    {
        //Do nothing
    }

    public void OnInteractHeld()
    {
        //Do nothing
    }

    public void OnLookingAt()
    {
        //Do nothing
    }


    private void Start()
    {
        isInteractable = true;
        m_anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Fire Air Event
        OnAirCannonTick.Invoke(airCannonPower * stage * Time.deltaTime);

        //Start particles if they are off and stage is above 0
        //stop particles if they are on and stage is equal to 0
    }

    private IEnumerator HoldingDownLever()
    {
        while (Input.GetKey(KeyCode.Mouse0))
        {
            if (Input.GetAxis("Mouse X") < 0 && stage < 5)
            {
                stage++;
                PlayRandomSqueek();
                m_anim.SetInteger("State", stage);
                break;
            }
            else if (Input.GetAxis("Mouse X") > 0 && stage > 0)
            {
                stage--;
                PlayRandomSqueek();
                m_anim.SetInteger("State", stage);
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void PlayRandomSqueek()
    {
        int randomNum = Random.Range(1, 4);

        switch (randomNum)
        {
            case 1: 
                AudioManager.instance.PlaySFX(eSFX.valvesqueek1, 0.2f);
                return;
            case 2:
                AudioManager.instance.PlaySFX(eSFX.valvesqueek2, 0.2f);
                return;
            case 3:
                AudioManager.instance.PlaySFX(eSFX.valvesqueek3, 0.2f);
                return;
        }
    }
}
