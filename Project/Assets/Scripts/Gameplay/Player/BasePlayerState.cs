using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    ChooseSpellForThisTurn,
    Casting,
    CastingSpellFromBigScroll,
    CastingSpellFromSmallScroll,
    UsingItem,
    Deactivated
}

public abstract class BasePlayerState
{
    public abstract PlayerState State { get; }
    public abstract void StartState(Player player);
    public abstract void EndState(Player player);
    public abstract void RunUpdate(Player player, GameplayController gameplayController);
}
