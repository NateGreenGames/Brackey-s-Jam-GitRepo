using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrottleLevelBehavior : ElectricityUser, IInteractable
{
    public bool isInteractable { get; set; }
    [SerializeField] float movementSpeed;
    [SerializeField] AudioSource engineRunningSource;
    [SerializeField] float leverSensitivity;
    private Animator m_anim;

    [SerializeField][ColorUsage(false, true)] Color middleColor, descendingColor;
    [SerializeField] private Material m_mat;
    void Start()
    {
        isInteractable = true;
        m_anim = GetComponent<Animator>();
        isOn = false;
        m_mat.SetColor("_EmissionColor", middleColor);
    }


    private void FixedUpdate()
    {
        if (isOn)
        {
            ProgressionManager.MoveSubmarine(movementSpeed * Time.fixedDeltaTime);
        }
    }
    void Update()
    {

        if (isOn && !engineRunningSource.isPlaying)
        {
            engineRunningSource.Play();
        }
        else if(!isOn)
        {
            engineRunningSource.Stop();
        }
    }
    public void OnInteract()
    {
        if (FuseBox.instance.isOverloaded) return;
        StartCoroutine(HoldingDownLever());
    }

    public void OnInteractEnd()
    {
    }

    public void OnInteractHeld()
    {
    }

    public void OnLookingAt()
    {
    }

    public override void ChangeActiveState(bool _state)
    {
        base.ChangeActiveState(_state);
        AudioManager.instance.PlaySFX(eSFX.leverPushPull, 0.2f);
        if (_state)
        {
            m_anim.SetTrigger("Down");
            m_mat.SetColor("_EmissionColor", descendingColor);
        }
        else
        {
            m_anim.SetTrigger("Up");
            m_mat.SetColor("_EmissionColor", middleColor);
        }
    }

    public override void OnOverload()
    {
        base.OnOverload();

        AudioManager.instance.PlaySFX(eSFX.leverPushPull, 0.2f);
        m_anim.SetTrigger("Up");
    }
    public IEnumerator HoldingDownLever()
    {
        while (Input.GetKey(KeyCode.Mouse0))
        {
            if (Input.GetAxis("Mouse Y") < 0 && !isOn)
            {
                m_anim.SetTrigger("Down");
                ChangeActiveState(true);
                break;
            }else if (Input.GetAxis("Mouse Y") > 0 && !isOn)
            {
                m_anim.SetTrigger("Bump");
                break;
            }
            else if (Input.GetAxis("Mouse Y") > 0 && isOn)
            {
                m_anim.SetTrigger("Up");
                ChangeActiveState(false);
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
