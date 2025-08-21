using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : EnemyNormal
{
    protected int range;

    public override GameplayObjectType GameplayObjectType => GameplayObjectType.EnemyRanged;

    public EnemyRanged(Sprite gameplayImage, TooltipParagraph[] descriptionEN, TooltipParagraph[] descriptionPL,
        int attributeHealthValue, int attributeMovementSpeedValue, int attributeDamageValue, int notImmobilised, LootSpawner lootSpawner, int defence, EnemyTier tier, int range) 
        : base(gameplayImage, descriptionEN, descriptionPL, attributeHealthValue, attributeMovementSpeedValue, attributeDamageValue, notImmobilised, lootSpawner, defence, tier)
    {
        this.range = range;
    }

    public override void PerformEnemyTurnAction()
    {
        if (attributes[AttributeID.NotImmobilised].CurrentValue > 0)
        {
            MoveAndCheckIfRemove(Map.Instance.MapData);
            if (!removeFromGame && attributes[AttributeID.Health].CurrentValue > 0) Shoot(Map.Instance.MapData);
        }
    }

    public override void PerformActionAtStartOfPlayerTurn()
    {
    }

    public override void PerformActionAtEndOfPlayerTurn()
    {
    }

    #region Shooting
    protected bool CheckIfInShootingRange(Tile[,] mapData)
    {
        bool inRange = false;
        if(tile.Position.Y - range <= 0)
        {
            inRange = true;
        }

        return inRange;
    }

    protected void Shoot(Tile[,] mapData)
    {
        if(CheckIfInShootingRange(mapData))
        {
            DealDamageToPlayer();
        }
    }

    public override void PerformEnemyTurnAction(int enemyMaxSpeed)
    {
        throw new NotImplementedException();
    }

    public static explicit operator EnemyRanged(EnemyData v)
    {
        throw new NotImplementedException();
    }

    #endregion
}
