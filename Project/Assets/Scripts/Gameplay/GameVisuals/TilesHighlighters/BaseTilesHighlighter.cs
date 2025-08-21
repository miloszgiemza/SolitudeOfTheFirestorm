using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTilesHighlighter : MonoBehaviour
{
    public MapPosition[] HighlightedTiles => highlightedTiles;

    protected MapPosition[] highlightedTiles;
    bool selectionActive = false;

    #region Pooler
    [SerializeField] protected GameObject tileHighlightPrefab;

    protected TileHighlight[,] tilesHighlights;
    #endregion

    protected void Awake()
    {
        MakeASingleton();
    }

    protected void Start()
    {
        CreateTilesHighlights(Map.Instance.MapData);
    }

    protected void OnDestroy()
    {
        UnmakeSingleton();
    }

    protected abstract void MakeASingleton();
    protected abstract void UnmakeSingleton();

    protected virtual void CreateTilesHighlights(Tile[,] mapData)
    {
        tilesHighlights = new TileHighlight[mapData.GetLength(0), mapData.GetLength(1)];

        for (int x = 0; x < tilesHighlights.GetLength(0); x++)
        {
            for (int y = 0; y < tilesHighlights.GetLength(1); y++)
            {
                tilesHighlights[x, y] =
                    Instantiate(tileHighlightPrefab, GameWorldToMapCastController.Instance.CastMapPosToGameWorld(new MapPosition(x, y)), Quaternion.identity, this.transform).GetComponent<TileHighlight>();

                tilesHighlights[x, y].Construct(x, y);

                tilesHighlights[x, y].gameObject.SetActive(false);
            }
        }
    }

    public virtual void UpdateHighlightedTiles(List<MapPosition> selectedTiles)
    {
        for (int i = 0; i < selectedTiles.Count; i++)
        {
            if (!tilesHighlights[selectedTiles[i].X, selectedTiles[i].Y].gameObject.activeSelf)
            {
                tilesHighlights[selectedTiles[i].X, selectedTiles[i].Y].gameObject.SetActive(true);
            }

            tilesHighlights[selectedTiles[i].X, selectedTiles[i].Y].RefreshLifetime();
        }
    }
}
