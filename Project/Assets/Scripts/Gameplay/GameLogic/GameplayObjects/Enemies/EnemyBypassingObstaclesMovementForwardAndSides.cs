using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;

public class EnemyBypassingObstaclesMovementForwardAndSides : EnemyNormal
{
    protected new MapPosition[] movementDirections = { new MapPosition(0, 1), new MapPosition(1, 1), new MapPosition(-1, 1), new MapPosition(-1, 0), new MapPosition(1, 0) };

    public EnemyBypassingObstaclesMovementForwardAndSides(Sprite gameplayImage, TooltipParagraph[] descriptionEN, TooltipParagraph[] descriptionPL, 
        int attributeHealthValue, int attributeMovementSpeedValue, int attributeDamageValue, int notImmobilised, LootSpawner lootSpawner, int defence, EnemyTier tier) : 
        base(gameplayImage, descriptionEN, descriptionPL, attributeHealthValue, attributeMovementSpeedValue, attributeDamageValue, notImmobilised, lootSpawner, defence, tier) { }

    public override void PerformActionAtEndOfPlayerTurn() { }

    public override void PerformActionAtStartOfPlayerTurn() { }

    public override void PerformEnemyTurnAction(int enemyForcedSpeed)
    {
        MoveAndCheckIfRemove(Map.Instance.MapData, enemyForcedSpeed);
    }

    protected override void MoveAndCheckIfRemove(Tile[,] mapData)
    {
        if (attributes[AttributeID.NotImmobilised].CurrentValue > 0)
        {
            for (int i = 0; i < attributes[AttributeID.MovementSpeed].CurrentValue; i++)
            {
                if (CheckIfReachedPlayer(tile.Position.X, tile.Position.Y - 1, mapData))
                {
                    DealDamageToPlayer();
                    attributes[AttributeID.Health].SetCurrentAttributeValue(0);
                    //tile.Clear(this);
                    removeFromGame = true;
                }
                else
                {
                    bool movementPerformed = false;

                    List<PositionInGrid> path = GetOptimalPath(MapPathfinding.Instance.NodesEnemyWalking);

                    #region MovementItself
                    if (!ReferenceEquals(path, null) && path.Count > 0)
                    {
                        tile.Clear(this);
                        mapData[path[0].X, path[0].Y].UpdateTile(this);
                        tile = mapData[path[0].X, path[0].Y];

                        PerformInteractionsWithOtherObjectsOnTileOnEnteringTile(tile);

                        movementPerformed = true;
                    }
                    #endregion

                    if (!movementPerformed)
                    {
                        tile.NodeEnemyWalking.SetTraversable(false);
                    }
                }
            }
        }
    }

    protected void MoveAndCheckIfRemove(Tile[,] mapData, int enemyForcedSpeed)
    {
        if (attributes[AttributeID.NotImmobilised].CurrentValue > 0)
        {
            for (int i = 0; i < enemyForcedSpeed; i++)
            {
                if (CheckIfReachedPlayer(tile.Position.X, tile.Position.Y - 1, mapData))
                {
                    DealDamageToPlayer();
                    attributes[AttributeID.Health].SetCurrentAttributeValue(0);
                    //tile.Clear(this);
                    removeFromGame = true;
                }
                else
                {
                    bool movementPerformed = false;

                    List<PositionInGrid> path = GetOptimalPath(MapPathfinding.Instance.NodesEnemyWalking);

                    #region MovementItself
                    if (!ReferenceEquals(path, null) && path.Count > 0)
                    {
                        tile.Clear(this);
                        mapData[path[0].X, path[0].Y].UpdateTile(this);
                        tile = mapData[path[0].X, path[0].Y];

                        PerformInteractionsWithOtherObjectsOnTileOnEnteringTile(tile);

                        movementPerformed = true;
                    }
                    #endregion

                    if (!movementPerformed)
                    {
                        tile.NodeEnemyWalking.SetTraversable(false);
                    }
                }
            }
        }
    }

    protected List<PositionInGrid> GetOptimalPath(Node[,] nodes)
    {
        tile.NodeEnemyWalking.SetTraversable(true);

        GraphSquaresDiagonalMovementForwardAndSides graph = new GraphSquaresDiagonalMovementForwardAndSides(MapPathfinding.Instance.NodesEnemyWalking);

        List<List<PositionInGrid>> paths = new List<List<PositionInGrid>>();

        for (int i = 0; i < nodes.GetLength(0); i++)
        {
            Debug.Log("Node " + tile.Position.X + " / " + tile.Position.Y + " neighbours: " + graph.Nodes[tile.Position.X, tile.Position.Y].Neighbours.Count);
            SearchAStarRealTileCost search = new SearchAStarRealTileCost(graph, graph.Nodes[tile.Position.X, tile.Position.Y], graph.Nodes[i, 0]);

            if (search.SearchAndReturnPath() != null)
            {
                List<PositionInGrid> newPath = search.SearchAndReturnPath();

                if (newPath.Count > 0) paths.Add(newPath);
            }
        }

        List<PositionInGrid> shortestPath = new List<PositionInGrid>(); 
        
        if(!ReferenceEquals(paths, null) && paths.Count > 0)
        {
            shortestPath = paths[0];

            for (int i = 1; i < paths.Count; i++)
            {
                if (paths[i].Count < shortestPath.Count && paths[i].Count > 0)
                {
                    shortestPath = paths[i];
                }
            }
        }
            

        return shortestPath;
    }
}
