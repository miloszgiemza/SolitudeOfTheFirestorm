using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesNormalAtributesUIDisplayController : BaseEnemiesStatsUIDispalayController
{
    public static EnemiesNormalAtributesUIDisplayController Instance => instance;
    private static EnemiesNormalAtributesUIDisplayController instance;

    public override void MakeSingletion()
    {
       if(!ReferenceEquals(EnemiesNormalAtributesUIDisplayController.Instance, null))
        {
            Destroy(this);
        }
       else
        {
            instance = this;
        }
    }

    public override void UnmakeSingleton()
    {
        instance = null;
    }

    public override void UpdateUI(Tile[,] mapData)
    {
        for(int x = 0; x < enemyAtributesDisplayers.GetLength(0); x++)
        {
            for(int y=0; y < enemyAtributesDisplayers.GetLength(1); y++)
            {
                if(!ReferenceEquals(mapData[x, y].EnemySocket, null))
                {
                    if (!enemyAtributesDisplayers[x, y].gameObject.activeSelf) enemyAtributesDisplayers[x, y].gameObject.SetActive(true);

                    enemyAtributesDisplayers[x, y].UpdateUI(mapData[x, y].EnemySocket.Attributes[AttributeID.Health].CurrentValue, mapData[x, y].EnemySocket.Attributes[AttributeID.MovementSpeed].CurrentValue,
                    mapData[x, y].EnemySocket.Attributes[AttributeID.Damage].CurrentValue, mapData[x, y].EnemySocket.Attributes[AttributeID.Defence].CurrentValue);
                }
                else
                {
                    enemyAtributesDisplayers[x, y].gameObject.SetActive(false);
                }
            }
        }
    }
}
