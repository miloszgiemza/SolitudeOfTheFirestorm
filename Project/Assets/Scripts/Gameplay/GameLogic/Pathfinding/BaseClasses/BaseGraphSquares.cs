using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public abstract class BaseGraphSquares : BaseGraph
    {
        public BaseGraphSquares(Node[,] mapData) : base(mapData)
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

        public override float GetNodesDistance(Node startNode, Node targetNode)
        {
            int distanceX = (int)Mathf.Abs(startNode.Position.X - targetNode.Position.X);
            int distanceY = (int)Mathf.Abs(startNode.Position.Y - targetNode.Position.Y);

            int min = Mathf.Min(distanceX, distanceY);
            int max = Mathf.Max(distanceX, distanceY);

            int diagonalPathLength = min;
            int straightPathLength = max - min;

            return (diagonalPathLength + straightPathLength);
        }

        public override void UpdateGraph(PositionInGrid nodePos, Node[,] mapData)
        {
            nodes[nodePos.X, nodePos.Y].Initialize(nodePos.X, nodePos.Y, Convert.ToBoolean(mapData[nodePos.X, nodePos.Y].Traversable), mapData[nodePos.X, nodePos.Y].MovementCostReal, mapData[nodePos.X, nodePos.Y].MovementCostInHeuristic);

            foreach (PositionInGrid direction in MovementDirections)
            {
                if (IsWithinBounds((nodePos.X + direction.X), (nodePos.Y + direction.Y))) nodes[(nodePos.X + direction.X), (nodePos.Y + direction.Y)].UpdateNeighbours(GetNeighbours((int)(nodePos.X + direction.X), (nodePos.Y + direction.Y)));
            }
        }

        protected override List<Node> GetNeighbours(int nodeX, int nodeY)
        {
            List<Node> neighbourNodes = new List<Node>();

            foreach (PositionInGrid dir in MovementDirections)
            {
                int neighboursX = nodeX + dir.X;
                int neighboursY = nodeY + dir.Y;

                if (IsWithinBounds(neighboursX, neighboursY) && nodes[neighboursX, neighboursY].Traversable)
                {
                    neighbourNodes.Add(nodes[neighboursX, neighboursY]);
                }
            }

            Debug.Log("Neighbours: " + neighbourNodes.Count);
            return neighbourNodes;
        }
    }
}

