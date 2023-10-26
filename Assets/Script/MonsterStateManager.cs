using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStateManager : MonoBehaviour
{
    public MonsterBaseState currentState;
    public MonsterIdleState idleState = new MonsterIdleState();
    public EyeballState eyeballState = new EyeballState();
    public TentacleMonsterState tentacleMonsterState = new TentacleMonsterState();
    public WormMonsterState wormMonsterState = new WormMonsterState();

    private void OnEnable()
    {
        idleState.OnEnable();
        eyeballState.OnEnable();
        tentacleMonsterState.OnEnable();
    }

    private void OnDisable()
    {
        idleState.OnDisable();
        eyeballState.OnDisable();
        tentacleMonsterState.OnDisable();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = idleState; //Sets initial state to idle
        currentState.EnterState(this); //Calls EnterState function over in MonsterIdleState script
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchStates(MonsterBaseState _state)
    {
        currentState = _state;
        _state.EnterState(this);
    }

    public void Enrage()
    {
        //Make whatever creature is currently attacking crush the sub, if it's idle, make a new creature appear and have them crush it.
    }
}
