using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesController : MonoBehaviour
{
    public static ObstaclesController Instance => instance;
    private static ObstaclesController instance;

    public List<GameplayObject> ObstaclesList => obstaclesList;

    private List<GameplayObject> obstaclesList = new List<GameplayObject>();

    private void Awake()
    {
        if(!ReferenceEquals(ObstaclesController.Instance, null))
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void RunObstacles()
    {
        List<GameplayObject> updatedObstaclesList = new List<GameplayObject>();

        for(int currentObstacle = 0; currentObstacle < obstaclesList.Count; currentObstacle++)
        {
            obstaclesList[currentObstacle].PerformActionAtStartOfPlayerTurn();

            if(obstaclesList[currentObstacle].RemoveFromGame)
            {
                obstaclesList[currentObstacle].Tile.Clear((Obstacle) obstaclesList[currentObstacle]);
            }
            else
            {
                updatedObstaclesList.Add(obstaclesList[currentObstacle]);
            }
        }

        obstaclesList = updatedObstaclesList;

        ObstaclesDisplayController.Instance.Display(ObstaclesList);
    }

    public void AddSpawnedObstacle(Obstacle newObstacle)
    {
        obstaclesList.Add(newObstacle);
    }
}
