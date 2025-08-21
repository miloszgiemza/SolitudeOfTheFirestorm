using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWithAreaAura : EnemyNormal
{
    public override GameplayObjectType GameplayObjectType => GameplayObjectType.EnemyWithAreaAura;

    protected List<Status> statuses = new List<Status>();
    protected MapPosition[] areaOfEffect;

    public EnemyWithAreaAura(Sprite gameplayImage, TooltipParagraph[] descriptionEN, TooltipParagraph[] descriptionPL, int attributeHealthValue, int attributeMovementSpeedValue, 
        int attributeDamageValue, int notImmobilised, LootSpawner lootSpawner, int defence, EnemyTier tier, List<Status> statuses, MapPosition[] areaOfEffect) 
        : base(gameplayImage, descriptionEN, descriptionPL, attributeHealthValue, attributeMovementSpeedValue, attributeDamageValue, notImmobilised, lootSpawner, defence, tier) 
    {
        this.statuses = statuses;
        this.areaOfEffect = areaOfEffect;
    }

    public override void PerformActionAtStartOfPlayerTurn()
    {
    }

    public override void PerformActionAtEndOfPlayerTurn()
    {
        GetEnemiesInAreaOfEffectAndTryToApplyStatuses();
    }

    protected void GetEnemiesInAreaOfEffectAndTryToApplyStatuses()
    {
        List<Tile> affectedTiles = ReturnAffectedTiles(tile.Position, Map.Instance.MapData);

        for(int currentAffectedTile = 0; currentAffectedTile < affectedTiles.Count; currentAffectedTile++)
        {
            affectedTiles[currentAffectedTile].MakeObjectsOnTileReact(statuses);
        }
    }

    private bool CheckIfPositionWithinMap(int x, int y, Tile[,] mapData)
    {
        return (x >= 0 && x < mapData.GetLength(0) && y >= 0 && y < mapData.GetLength(1));
    }

    public List<Tile> ReturnAffectedTiles(MapPosition auraStartPosition, Tile[,] mapData)
    {
        List<Tile> affectedTiles = new List<Tile>();


        for (int auraCurrentDirection = 0; auraCurrentDirection < areaOfEffect.Length; auraCurrentDirection++)
        {
            if (CheckIfPositionWithinMap(auraStartPosition.X + areaOfEffect[auraCurrentDirection].X, auraStartPosition.Y
                + areaOfEffect[auraCurrentDirection].Y, mapData))
            {
                affectedTiles.Add(mapData[auraStartPosition.X + areaOfEffect[auraCurrentDirection].X, auraStartPosition.Y
                    + areaOfEffect[auraCurrentDirection].Y]);
            }
        }

        return affectedTiles;
    }
}
