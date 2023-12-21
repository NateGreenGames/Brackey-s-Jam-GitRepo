using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCannonValve : MonoBehaviour
{
    public delegate void Blank(float _power);
    public static event Blank OnAirCannonTick;

    [SerializeField][Range(0, 5)] private int stage;
    [SerializeField] private float airCannonPower;
    [SerializeField] private float oxygenUsedPerSecond;
    [SerializeField] private GameObject m_Part;

    private ParticleSystem.MainModule particalMain;
    private Animator m_anim;
    private AudioSource m_audi;
  


    private void Start()
    {
        m_anim = GetComponent<Animator>();
        m_audi = GetComponent<AudioSource>();
        particalMain = m_Part.GetComponent<ParticleSystem>().main;
    }

    void Update()
    {
        //Fire Air Event
        OnAirCannonTick?.Invoke(airCannonPower * Mathf.Pow(stage, 2) * Time.deltaTime);
        OxygenManagement.ChangeOxygenAmount(-oxygenUsedPerSecond * Mathf.Pow(stage, 2) * Time.deltaTime);


        m_audi.volume = 0.2f * stage;
        if(!m_audi.isPlaying && stage > 0)
        {
            m_audi.Play();
        }
        else if(m_audi.isPlaying && stage == 0)
        {
            m_audi.Stop();
        }
    }
    public void StageUp()
    {
        if(stage != 5)
        {
            stage++;
            particalMain.startSize = stage;
            PlayRandomSqueek();
            m_anim.SetInteger("State", stage);
        }
    }

    public void StageDown()
    {
        if(stage != 0)
        {
            stage--;
            particalMain.startSize = stage;
            PlayRandomSqueek();
            m_anim.SetInteger("State", stage);
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
