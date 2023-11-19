using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubArrowBehavior : MonoBehaviour
{
    [SerializeField] float blinksPerSecond;
    [SerializeField] ElectricityUser relatedObject;
    [SerializeField] Vector3[] arrowPositions;

    [SerializeField]private int currentPositionIndex = 0;
    private float timer;
    MeshRenderer m_mr;


    private void Start()
    {
        m_mr = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (relatedObject.isOn)
        {
            timer += Time.deltaTime;
            if(timer >= blinksPerSecond)
            {
                timer = 0;
                m_mr.enabled = !m_mr.enabled;
                if (m_mr.enabled)
                {
                    transform.localPosition = arrowPositions[currentPositionIndex];
                    currentPositionIndex++;
                    if (currentPositionIndex == arrowPositions.Length)
                    {
                        currentPositionIndex = 0;
                    }
                }
            }
        }
        else
        {
            m_mr.enabled = false;
            currentPositionIndex = 0;
        }

    }
}
