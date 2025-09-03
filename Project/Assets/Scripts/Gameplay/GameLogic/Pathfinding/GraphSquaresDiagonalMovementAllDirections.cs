using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class GraphSquaresDiagonalMovementAllDirections : BaseGraphSquares
    {
        public override List<PositionInGrid> MovementDirections => new List<PositionInGrid>
        {
            new PositionInGrid(0, 1),
            new PositionInGrid(1, 1),
            new PositionInGrid(1, 0),
            new PositionInGrid(1, -1),
            new PositionInGrid(0, -1),
            new PositionInGrid(-1, -1),
            new PositionInGrid(-1, 0),
            new PositionInGrid(-1, 1)
        };

        public GraphSquaresDiagonalMovementAllDirections(Node[,] mapData) : base(mapData)
        {
            nodes = new Node[mapData.GetLength(0), mapData.GetLength(1)];

            for (int y = 0; y < nodes.GetLength(1); y++)
            {
                for (int x = 0; x < nodes.GetLength(0); x++)
                {
                    Node newNode = new Node();
                    newNode.Initialize(x, y, Convert.ToBoolean(mapData[x, y].Traversable), mapData[x, y].MovementCostReal, mapData[x, y].MovementCostInHeuristic);
                    nodes[x, y] = newNode;
                }
            }

            for (int y = 0; y < mapData.GetLength(1); y++)
            {
                for (int x = 0; x < mapData.GetLength(0); x++)
                {
                    if (nodes[x, y].Traversable)
                    {
                        nodes[x, y].UpdateNeighbours(GetNeighbours(x, y));
                    }
                }
            }
        }
    }
}
