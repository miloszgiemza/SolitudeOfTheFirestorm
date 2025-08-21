using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDrinkMixture : BasePlayerState
{
    public override PlayerState State => PlayerState.DrinkingMixture;

    protected Player player;

    public override void StartState(Player player)
    {
        this.player = player;
        PlayerStateDrinkMixtureEvents.OnDrinMixture += DrinkMixture;
    }

    public override void RunUpdate(Player player, GameplayController gameplayController)
    {
    }

    public override void EndState(Player player)
    {
        PlayerStateDrinkMixtureEvents.OnDrinMixture -= DrinkMixture;
    }

    public void DrinkMixture(ItemMixture mixture)
    {
        player.SpendSecendaryAction();

        player.AcquireStatuses(mixture.Statuses);

        PlayerInventoryController.Instance.PlayerInventory.DestroyItemAndNullCurrentItemOnSuccesfullUse();

        player.SwitchState(PlayerState.Idle);
    }
}

public static class PlayerStateDrinkMixtureEvents
{
        public static Action<ItemMixture> OnDrinMixture;
}
