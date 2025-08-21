using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public abstract class BaseGraph
    {
        public Node[,] Nodes => nodes;

        protected Node[,] nodes;

        public abstract List<PositionInGrid> MovementDirections { get; }

        public BaseGraph(Node[,] mapData)
        {
        }

        public bool IsWithinBounds(int x, int y)
        {
            return (x >= 0 && x < nodes.GetLength(0) && y >= 0 && y < nodes.GetLength(1));
        }

        protected abstract List<Node> GetNeighbours(int nodeX, int nodeY);


        public abstract void UpdateGraph(PositionInGrid nodePos, Node[,] mapData);

        public abstract float GetNodesDistance(Node startNode, Node targetNode);
    }
}

