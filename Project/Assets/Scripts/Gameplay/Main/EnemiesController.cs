using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public static EnemiesController Instance => instance;

    private static EnemiesController instance;

    public List<GameplayObject> EnemiesOnMap => enemiesOnMap;

    private List<GameplayObject> enemiesOnMap = new List<GameplayObject>();
    
    private void Awake()
    {
        if(EnemiesController.Instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void OnDisable()
    {
        instance = null;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void RunEnemies(GameplayController game)
    {
        enemiesOnMap.Sort();

        for (int currentEnemy = 0; currentEnemy < enemiesOnMap.Count; currentEnemy++)
        {
            //Debug.Log("Current enemy position: X" + enemiesOnMap[currentEnemy].Tile.Position.X + " Y" + enemiesOnMap[currentEnemy].Tile.Position.Y);
            enemiesOnMap[currentEnemy].ApplyStatuses();

            enemiesOnMap[currentEnemy].PerformEnemyTurnAction();
        }

        EnemiesController.Instance.ClearDeadEnemies();
    }

    public void RunEnemies(GameplayController game, int enemiesForcedSpeed)
    {
        enemiesOnMap.Sort();

        for (int currentEnemy = 0; currentEnemy < enemiesOnMap.Count; currentEnemy++)
        {
            //Debug.Log("Current enemy position: X" + enemiesOnMap[currentEnemy].Tile.Position.X + " Y" + enemiesOnMap[currentEnemy].Tile.Position.Y);
            enemiesOnMap[currentEnemy].ApplyStatuses();

            enemiesOnMap[currentEnemy].PerformEnemyTurnAction(enemiesForcedSpeed);
        }

        EnemiesController.Instance.ClearDeadEnemies();
    }

    public void RunEnemiesActionAtEndOfPlayerTurn()
    {
        EnemiesController.Instance.ClearDeadEnemies();

        for (int currentEnemy = 0; currentEnemy < enemiesOnMap.Count; currentEnemy++)
        {
            enemiesOnMap[currentEnemy].PerformActionAtEndOfPlayerTurn();
        }
    }

    public void ClearDeadEnemies()
    {
        List<GameplayObject> updatedEnemiesList = new List<GameplayObject>();

        for(int i = 0; i < enemiesOnMap.Count; i++)
        {
            if (enemiesOnMap[i].RemoveFromGame)
            {
                enemiesOnMap[i].Tile.Clear(enemiesOnMap[i].GameplayObjectType);
            }
            else
            {
                updatedEnemiesList.Add(enemiesOnMap[i]);
            }
        }

        enemiesOnMap = updatedEnemiesList;

        EnemiesDisplayControler.Instance.Display(this.EnemiesOnMap);
    }

    public void ResetEnemyActions()
    {

    }

    public void AddSpawnedEnemy(GameplayObject newObject)
    {
        enemiesOnMap.Add(newObject);
    }

    public bool CheckIfNoEnemiesOnMap()
    {
        bool noEnemiesOnMap = false;
        
        if(enemiesOnMap.Count <= 0)
        {
            noEnemiesOnMap = true;
        }

        return noEnemiesOnMap;
    }
}
