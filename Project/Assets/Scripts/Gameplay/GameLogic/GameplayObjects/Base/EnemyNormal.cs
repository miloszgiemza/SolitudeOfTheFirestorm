using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyNormal : BaseEnemy
{
    public override GameplayObjectType GameplayObjectType => GameplayObjectType.Enemy; 

    public EnemyNormal(Sprite gameplayImage, TooltipParagraph[] descriptionEN, TooltipParagraph[] descriptionPL,
        int attributeHealthValue, int attributeMovementSpeedValue, int attributeDamageValue, int notImmobilised, LootSpawner lootSpawner, int defence, EnemyTier tier) : 
        base(gameplayImage, descriptionEN, descriptionPL, attributeHealthValue, attributeMovementSpeedValue, attributeDamageValue, notImmobilised, lootSpawner, defence, tier) 
    {
    }

    protected override void PerformInteractionsWithOtherObjectsOnTileOnEnteringTile(Tile tile)
    {
        if (!ReferenceEquals(tile.ObstacleSocket, null))
        { 
            attributes[AttributeID.Health].SubstractFromCurrentAttributeValue(tile.ObstacleSocket.Attributes[AttributeID.Damage].CurrentValue);
            AcquireStatusesNotFromPlayer(tile.ObstacleSocket.Statuses);

            if(attributes[AttributeID.Health].CurrentValue <= 0)
            {
                removeFromGame = true;
            }
            //EnemiesController.Instance.ClearDeadEnemies();
        }
    }

    public override void PerformEnemyTurnAction()
    {
       if(attributes[AttributeID.NotImmobilised].CurrentValue>0)
        {
            MoveAndCheckIfRemove(Map.Instance.MapData);
        } 
    }

    public override void PerformEnemyTurnAction(int enemyForcedSpeed)
    {
            MoveAndCheckIfRemove(Map.Instance.MapData, enemyForcedSpeed);
    }

    #region Movement

    protected bool CheckIfReachedPlayer(int x, int y, Tile[,] mapData)
    {
        return (x >= 0 && x < mapData.GetLength(0) && y < 0);
    }

    protected void AttackPlayerDestroySelfAndFreeTile(ref bool removeAfterTurn)
    {
        tile.Clear(this);
        //Deal damage to player
        removeAfterTurn = true;

        EnemiesController.Instance.ClearDeadEnemies();
    }

    protected virtual void MoveAndCheckIfRemove(Tile[,] mapData)
    {
        if(attributes[AttributeID.NotImmobilised].CurrentValue > 0)
        {
            for (int i = 0; i < attributes[AttributeID.MovementSpeed].CurrentValue; i++)
            {
                bool movementPerformed = false;

                for (int d = 0; d < movementDirections.Length && !movementPerformed; d++)
                {
                    if ((Arrays2DExtensions.CheckIfPositionIsWithinBoundsOfArray<Tile>(tile.Position.X + movementDirections[d].X, tile.Position.Y + movementDirections[d].Y, mapData))
                        && mapData[tile.Position.X + movementDirections[d].X, tile.Position.Y + movementDirections[d].Y].CheckIfPossibleToMoveOn(this))
                    {
                        tile.Clear(this);
                        mapData[tile.Position.X + movementDirections[d].X, tile.Position.Y + movementDirections[d].Y].UpdateTile(this);
                        tile = mapData[tile.Position.X + movementDirections[d].X, tile.Position.Y + movementDirections[d].Y];

                        PerformInteractionsWithOtherObjectsOnTileOnEnteringTile(tile);

                        movementPerformed = true;
                    }
                    else if (CheckIfReachedPlayer(tile.Position.X + movementDirections[d].X, tile.Position.Y + movementDirections[d].Y, mapData))
                    {
                        DealDamageToPlayer();
                        attributes[AttributeID.Health].SetCurrentAttributeValue(0);
                        //tile.Clear(this);
                        removeFromGame = true;
                    }
                }
            }
        }
    }

    protected virtual void MoveAndCheckIfRemove(Tile[,] mapData, int enemyForcedSpeed)
    {
            for (int i = 0; i < enemyForcedSpeed; i++)
            {
                bool movementPerformed = false;

                for (int d = 0; d < movementDirections.Length && !movementPerformed; d++)
                {
                    if ((Arrays2DExtensions.CheckIfPositionIsWithinBoundsOfArray<Tile>(tile.Position.X + movementDirections[d].X, tile.Position.Y + movementDirections[d].Y, mapData))
                        && mapData[tile.Position.X + movementDirections[d].X, tile.Position.Y + movementDirections[d].Y].CheckIfPossibleToMoveOn(this))
                    {
                        tile.Clear(this);
                        mapData[tile.Position.X + movementDirections[d].X, tile.Position.Y + movementDirections[d].Y].UpdateTile(this);
                        tile = mapData[tile.Position.X + movementDirections[d].X, tile.Position.Y + movementDirections[d].Y];

                        PerformInteractionsWithOtherObjectsOnTileOnEnteringTile(tile);

                        movementPerformed = true;
                    }
                    else if (CheckIfReachedPlayer(tile.Position.X + movementDirections[d].X, tile.Position.Y + movementDirections[d].Y, mapData))
                    {
                        DealDamageToPlayer();
                        attributes[AttributeID.Health].SetCurrentAttributeValue(0);
                        //tile.Clear(this);
                        removeFromGame = true;
                    }
                }
            }
    }
    #endregion
}
