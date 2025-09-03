using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public abstract class BaseSearch
    {
        public List<Vector2> Path => path;

        protected Node startNode;
        protected Node endNode;
        protected BaseGraph graph;

        protected List<Node> frontierNodes; //used as queue

        protected List<Node> exploredNodes;
        protected List<Node> pathNodes;

        protected List<Vector2> path;

        public BaseSearch(BaseGraph graphValue, Node startNodeValue, Node endNodeValue)
        {
            startNode = startNodeValue;
            startNode.SetDistanceTravelled(0);

            endNode = endNodeValue;
            graph = graphValue;
        }

        protected void InitSearch()
        {

            frontierNodes = new List<Node>();
            frontierNodes.Add(startNode);
            exploredNodes = new List<Node>();
            pathNodes = new List<Node>();

            startNode.SetDistanceTravelled(0);
        }

        public abstract List<PositionInGrid> SearchAndReturnPath();
        protected abstract void ExpandFrontier(Node node);

        protected List<Node> GetPathNodes(Node endNode)
        {
            //go back on your steps and reverse list
            List<Node> path = new List<Node>();

            path.Add(endNode);

            Node currentNode = endNode.PreviousNode;

            while (currentNode != null)
            {
                path.Insert(0, currentNode);
                currentNode = currentNode.PreviousNode;
            }

            return path;
        }
    }
}
