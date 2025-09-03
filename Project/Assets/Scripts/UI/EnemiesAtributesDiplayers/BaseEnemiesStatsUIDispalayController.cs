using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemiesStatsUIDispalayController : MonoBehaviour
{
    [SerializeField] protected GameObject enemyStatsDisplayerPrefab;

    protected EnemyAtributesDisplayer[,] enemyAtributesDisplayers;

    private void Awake()
    {
        MakeSingletion();
    }

    protected void Start()
    {
        Initialize(Map.Instance.MapData);
    }

    protected void OnDisable()
    {
        UnmakeSingleton();
    }

    protected void OnDestroy()
    {
        UnmakeSingleton();
    }

    public abstract void MakeSingletion();
    public abstract void UnmakeSingleton();

    protected void Initialize(Tile[,] mapData)
    {
        enemyAtributesDisplayers = new EnemyAtributesDisplayer[mapData.GetLength(0), mapData.GetLength(1)];

        for(int x = 0; x < enemyAtributesDisplayers.GetLength(0); x++)
        {
            for(int y=0; y < enemyAtributesDisplayers.GetLength(1); y++)
            {
                enemyAtributesDisplayers[x, y] = Instantiate(enemyStatsDisplayerPrefab, 
                    new Vector3(GameWorldToMapCastController.Instance.CastMapPosToGameWorld(x, y).x, GameWorldToMapCastController.Instance.CastMapPosToGameWorld(x, y).y, this.transform.position.z), 
                    Quaternion.identity, this.transform).GetComponent<EnemyAtributesDisplayer>();

                
            }
        }
    }

    public abstract void UpdateUI(Tile[,] mapData);
}
