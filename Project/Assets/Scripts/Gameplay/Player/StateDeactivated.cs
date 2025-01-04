using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDeactivated : BasePlayerState
{
    public override PlayerState State => PlayerState.Deactivated;

    public override void StartState(Player player)
    {
    }

    public override void EndState(Player player)
    {
    }

    public override void RunUpdate(Player player, GameplayController gameplayController)
    {
    }
}
