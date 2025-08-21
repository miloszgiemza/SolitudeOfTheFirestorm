using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class PositionInGrid
    {
        public int X => x;
        public int Y => y;

        private int x;
        private int y;
        public PositionInGrid(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class Node
    {
        public PositionInGrid Position => position;
        public bool Traversable => traversable;
        public float MovementCostReal => movementCostReal;
        public float MovementCostInHeuristic => movementCostInHeuristic;
        public List<Node> Neighbours => neighbours;
        public Node PreviousNode => previousNode;
        public float DistanceTravelled => distanceTravelled;

        protected PositionInGrid position;
        protected bool traversable = true;
        protected float movementCostReal = 0f;
        protected float movementCostInHeuristic = 0f;

        protected List<Node> neighbours = new List<Node>();
        protected Node previousNode = null;
        protected float distanceTravelled = Mathf.Infinity;

        public Node()
        {

        }

        public Node(int x, int y, bool traversable, float movementCostReal, float movementCostHeuristic)
        {
            Initialize(x, y, traversable, movementCostReal, movementCostHeuristic);
            
            Reset();
        }

        /*
        public void Initialize(int x, int y, bool traversable, float movementCostReal)
        {
            position = new PositionInGrid(x, y);
            this.traversable = traversable;
            this.movementCostReal = movementCostReal;
           
            Reset();
        }
        */

        public void Initialize(int x, int y, bool traversable, float movementCostReal, float movementCostInHeuristic)
        {
            position = new PositionInGrid(x, y);
            this.traversable = traversable;
            this.movementCostReal = movementCostReal;
            this.movementCostInHeuristic = movementCostInHeuristic;

            Reset();
        }

        public void Reset()
        {
            neighbours = new List<Node>();
            previousNode = null;
            distanceTravelled = Mathf.Infinity;
        }

        public void UpdateNeighbours(List<Node> newNeighbours)
        {
            neighbours = newNeighbours;
        }

        public void SetDistanceTravelled(float newDistanceTravelled)
        {
            distanceTravelled = newDistanceTravelled;
        }

        public void SetPreviousNode(Node node)
        {
            previousNode = node;
        }

        public void SetTraversable(bool traversable)
        {
            this.traversable = traversable;
        }

        public void SetMovementCostReal(float movementCostReal)
        {
            this.movementCostReal = movementCostReal;
        }

        public void SetMovementCostHeuristic(float movementCostHeuristic)
        {
            this.movementCostInHeuristic = movementCostHeuristic;
        }

        public static List<Node> SortNodesListByDistanceFromStartAscending(List<Node> nodes)
        {
            List<Node> unsortedNodes = nodes;
            List<Node> sortedNodes = new List<Node>();

            while (unsortedNodes.Count > 0)
            {
                Node min = unsortedNodes[0];
                int minIndex = 0;

                for (int i = 0; i < unsortedNodes.Count; i++)
                {
                    if (unsortedNodes[i].DistanceTravelled < min.DistanceTravelled)
                    {
                        min = unsortedNodes[i];
                        minIndex = i;
                    }
                }

                sortedNodes.Add(min);
                unsortedNodes.RemoveAt(minIndex);
            }

            return sortedNodes;
        }

        #region A*PriorityQueue
        public int Priority => priority;

        private int priority;

        public void SetPriority(int priorityValue)
        {
            priority = priorityValue;
        }

        public static List<Node> SortListByPriority(List<Node> nodes)
        {
            List<Node> unsortedNodes = nodes;
            List<Node> sortedNodes = new List<Node>();

            while (unsortedNodes.Count > 0)
            {
                Node min = unsortedNodes[0];
                int minIndex = 0;

                for (int i = 0; i < unsortedNodes.Count; i++)
                {
                    if (unsortedNodes[i].priority < min.priority)
                    {
                        min = unsortedNodes[i];
                        minIndex = i;
                    }
                }

                sortedNodes.Add(min);
                unsortedNodes.RemoveAt(minIndex);
            }

            return sortedNodes;
        }
        #endregion
    }
}
