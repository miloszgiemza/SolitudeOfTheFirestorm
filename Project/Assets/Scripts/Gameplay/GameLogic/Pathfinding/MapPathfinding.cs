using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class MapPathfinding : MonoBehaviour
    {
        public static MapPathfinding Instance => instance;
        private static MapPathfinding instance;

        public Node[,] NodesEnemyWalking => nodesEnemyWalking;
        public Node[,] NodesEnemyFlying => nodesEnemyFlying;

        private Node[,] nodesEnemyWalking;
        private Node[,] nodesEnemyFlying;

        private void Awake()
        {
            if(!ReferenceEquals(MapPathfinding.Instance, null))
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
        }

        private void Start()
        {
            Initialize(Map.Instance.MapData);
        }

        private void OnDisable()
        {
            instance = null;
        }

        private void OnDestroy()
        {
            instance = null;
        }

        private void Initialize(Tile[,] mapData)
        {
            nodesEnemyWalking = new Node[mapData.GetLength(0), mapData.GetLength(1)];

            for(int y = 0; y < nodesEnemyWalking.GetLength(1); y++)
            {
                for(int x = 0; x < nodesEnemyWalking.GetLength(0); x++)
                {
                    nodesEnemyWalking[x, y] = mapData[x, y].NodeEnemyWalking;
                    Debug.Log("Traversable: " + nodesEnemyWalking[x, y].Traversable);
                }
            }

            nodesEnemyFlying = new Node[mapData.GetLength(0), mapData.GetLength(1)];

            for (int y = 0; y < nodesEnemyFlying.GetLength(1); y++)
            {
                for (int x = 0; x < nodesEnemyFlying.GetLength(0); x++)
                {
                    nodesEnemyFlying[x, y] = mapData[x, y].NodeEnemyFlying;
                }
            }
        }
    }
}
