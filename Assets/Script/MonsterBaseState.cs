using UnityEngine;

public abstract class MonsterBaseState
{
    public abstract void OnEnable();
    public abstract void OnDisable();
    public abstract void EnterState(MonsterStateManager _monsterStateManager);
    public abstract void UpdateState(MonsterStateManager _monsterStateManager);
}
