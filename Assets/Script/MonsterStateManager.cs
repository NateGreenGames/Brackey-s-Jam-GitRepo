using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStateManager : MonoBehaviour
{
    MonsterBaseState currentState;
    MonsterIdleState idleState = new MonsterIdleState();
    EyeballState eyeballState = new EyeballState();

    // Start is called before the first frame update
    void Start()
    {
        currentState = idleState; //Sets initial state to idle
        currentState.EnterState(this); //Calls EnterState function over in MonsterIdleState script
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
