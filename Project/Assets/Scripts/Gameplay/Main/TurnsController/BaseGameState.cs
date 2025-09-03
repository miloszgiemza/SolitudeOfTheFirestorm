using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameState
{
    public abstract GameplayController.States StateID { get; }
    public abstract void StartTurn(GameplayController game);
    public abstract void RunUpdate(GameplayController game);
    public abstract void EndTurn(GameplayController game);
}
