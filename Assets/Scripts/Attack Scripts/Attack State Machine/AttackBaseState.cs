using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBaseState
{
    public abstract void EnterState(AttackStateManager manager);
    public abstract void UpdateState(AttackStateManager manager);
    public abstract void ExitState(AttackStateManager manager);

}
