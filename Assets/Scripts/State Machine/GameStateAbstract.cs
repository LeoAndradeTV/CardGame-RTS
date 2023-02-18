using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameStateAbstract
{
    public abstract void EnterState(GameStateManager manager);
    public abstract void UpdateState(GameStateManager manager);
    public abstract void ExitState(GameStateManager manager);

}
