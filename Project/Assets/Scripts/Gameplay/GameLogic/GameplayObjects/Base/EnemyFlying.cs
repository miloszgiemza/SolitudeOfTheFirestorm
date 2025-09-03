using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying : BaseEnemy
{
    public override GameplayObjectType GameplayObjectType => GameplayObjectType.EnemyFlying;

    public EnemyFlying(Sprite gameplayImage, TooltipParagraph[] descriptionEN, TooltipParagraph[] descriptionPL,
        int attributeHealthValue, int attributeMovementSpeedValue, int attributeDamageValue, int notImmobilised, LootSpawner lootSpawner, int defence, EnemyTier tier) 
        : base(gameplayImage, descriptionEN, descriptionPL, attributeHealthValue, attributeMovementSpeedValue, attributeDamageValue, notImmobilised, lootSpawner, defence, tier)
    {
    }

    protected override void PerformInteractionsWithOtherObjectsOnTileOnEnteringTile(Tile tile)
    {
    }

    public override void PerformEnemyTurnAction()
    {
    }

    public void PerformInteractionsWithOtherObjectsOnTileOnEnteringTile()
    {
    }

    public override void PerformActionAtStartOfPlayerTurn()
    {
    }

    public override void PerformActionAtEndOfPlayerTurn()
    {
    }

    public override void PerformEnemyTurnAction(int enemyMaxSpeed)
    {
        throw new System.NotImplementedException();
    }
}
