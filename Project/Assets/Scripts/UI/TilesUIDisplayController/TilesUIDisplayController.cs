using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesUIDisplayController : MonoBehaviour
{
    public static TilesUIDisplayController Instance => instance;
    protected static TilesUIDisplayController instance;

    public TileUIDisplayer[,] TilesUIDisplayers => tilesUIDisplayers;

    [SerializeField] protected GameObject tileUIDisplayerPrefab;

    protected TileUIDisplayer[,] tilesUIDisplayers;

    protected void Awake()
    {
        if (!ReferenceEquals(TilesUIDisplayController.Instance, null))
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    protected void Start()
    {
        Initialize(Map.Instance.MapData);
    }

    protected void OnDestroy()
    {
        instance = null;
    }

    protected void Initialize(Tile[,] mapData)
    {
        tilesUIDisplayers = new TileUIDisplayer[mapData.GetLength(0), mapData.GetLength(1)];

        for (int x = 0; x < tilesUIDisplayers.GetLength(0); x++)
        {
            for (int y = 0; y < tilesUIDisplayers.GetLength(1); y++)
            {
                tilesUIDisplayers[x, y] = Instantiate(tileUIDisplayerPrefab,
                    new Vector3(GameWorldToMapCastController.Instance.CastMapPosToGameWorld(x, y).x, GameWorldToMapCastController.Instance.CastMapPosToGameWorld(x, y).y, this.transform.position.z),
                    Quaternion.identity, this.transform).GetComponent<TileUIDisplayer>();
            }
        }
    } 
}
