using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Spells/SpellCreation", fileName = "NewSpellCreation")]
public class SpellAreaOfEffectCreation : BaseSpellAreaOfEffect
{
    [SerializeField] protected Sprite obstacleImage;
    [SerializeField] protected bool walkable = false;
    [SerializeField] protected int duration;

    public override List<Tile> ReturnAffectedTiles(Tile[,] mapData, Vector2 cursorPos, int modifierRange)
    {
        List<Tile> affectedTiles = new List<Tile>();


        if (GameWorldToMapCastController.Instance.CastGameWorldPosToMap(InputControllerGameplay.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>()).X >= 0)
        {
            MapPosition spellPosition = GameWorldToMapCastController.Instance.CastGameWorldPosToMap(cursorPos);

            for (int spellCurrentDirection = 0; spellCurrentDirection < ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect).Length; spellCurrentDirection++)
            {
                if (CheckIfPositionWithinMapAndSpellRange(spellPosition.X + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].X, 
                    spellPosition.Y + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].Y, mapData, rangeY, modifierRange) 
                    && mapData[spellPosition.X + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].X, 
                    spellPosition.Y + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].Y].CheckIfPossibleToCreateObbstacle() )
                {
                    affectedTiles.Add(mapData[spellPosition.X + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].X, spellPosition.Y 
                        + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].Y]);
                }
            }
        }

        return affectedTiles;
    }

    public override List<MapPosition> ReturnAffectedTilesPositions(Tile[,] mapData, Vector2 cursorPos, int modifierRange)
    {
        List<MapPosition> affectedTilesPositions = new List<MapPosition>();

        if (GameWorldToMapCastController.Instance.CastGameWorldPosToMap(InputControllerGameplay.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>()).X >= 0)
        {
            MapPosition spellPosition = GameWorldToMapCastController.Instance.CastGameWorldPosToMap(cursorPos);

            for (int spellCurrentDirection = 0; spellCurrentDirection < ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect).Length; spellCurrentDirection++)
            {
                if (CheckIfPositionWithinMapAndSpellRange(spellPosition.X + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].X, 
                    spellPosition.Y + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].Y, mapData, rangeY, modifierRange)
                    && mapData[spellPosition.X + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].X, 
                    spellPosition.Y + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].Y].CheckIfPossibleToCreateObbstacle())
                {
                    affectedTilesPositions.Add((mapData[spellPosition.X + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].X, spellPosition.Y 
                        + ReturnRotatedUpToDateAreaOfEffect(SpellsController.Instance.InvertedAreaOfEffect)[spellCurrentDirection].Y]).Position);
                }
            }

            ObstaclesDisplayController.Instance.Display(ObstaclesController.Instance.ObstaclesList);
        }

        return affectedTilesPositions;
    }

    public override bool TryCast(Tile[,] mapData, Vector2 cursorPos, int modifierDamage, int modifierRange, int modifierEffectLength)
    {
        bool spellCastedSuccesfully = false;

        if (GameWorldToMapCastController.Instance.CastGameWorldPosToMap(InputControllerGameplay.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>()).X >= 0
            && CheckIfPositionWithinMapAndSpellRange(GameWorldToMapCastController.Instance.CastGameWorldPosToMap
            (InputControllerGameplay.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>()).X,
            GameWorldToMapCastController.Instance.CastGameWorldPosToMap(InputControllerGameplay.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>()).Y,
            Map.Instance.MapData, rangeY, modifierRange))
        {
            spellCastedSuccesfully = true;

            List<Tile> affectedTiles = ReturnAffectedTiles(mapData, cursorPos, modifierRange);

            for (int currentTile = 0; currentTile < affectedTiles.Count; currentTile++)
            {
                if (affectedTiles[currentTile].CheckIfPossibleToCreateObbstacle())
                {
                    Obstacle newObstacle = new Obstacle(obstacleImage, walkable, damage, duration + modifierEffectLength, statuses, affectedTiles[currentTile]);
                    ObstaclesController.Instance.AddSpawnedObstacle(newObstacle);
                    affectedTiles[currentTile].UpdateTile(newObstacle);
                }
            }
        }

        return spellCastedSuccesfully;
    }
}