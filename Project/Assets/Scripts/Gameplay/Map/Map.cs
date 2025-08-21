using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class Map : MonoBehaviour
{
    public static Map Instance => instance;

    private static Map instance;

    public int EnemiesSpawningRowOnY => enemiesSpawningRowOnY;
    public int MapXSize => mapXSize;
    public int MapYSize => mapYSize;

    public Tile[,] MapData => mapData;

    private Tile[,] mapData;

    private int enemiesSpawningRowOnY = 14;
    [SerializeField] private int mapXSize = 8;
    [SerializeField] private int mapYSize = 15;

    private void Awake()
    {
        enemiesSpawningRowOnY = mapYSize - 1;

        if(!ReferenceEquals(Map.Instance, null))
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        Initialize(mapXSize, mapYSize);
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void Initialize(int mapXSize, int mapYSize)
    {
        mapData = new Tile[mapXSize, mapYSize];

        for(int x = 0; x < mapXSize; x++)
        {
            for (int y = 0; y < mapYSize; y++)
            {
                mapData[x, y] = new Tile(new MapPosition(x, y));
            }
        }
    }
}
