using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTilesHighlighter : MonoBehaviour
{
    public MapPosition[] HighlightedTiles => highlightedTiles;

    private MapPosition[] highlightedTiles;
    bool selectionActive = false;

    #region Pooler
    [SerializeField] private GameObject tileHighlightPrefab;

    private TileHighlight[,] tilesHighlights;
    #endregion

    private void Awake()
    {
        MakeASingleton();
    }

    private void Start()
    {
        CreateTilesHighlights(Map.Instance.MapData);
    }

    protected abstract void MakeASingleton();

    private void CreateTilesHighlights(Tile[,] mapData)
    {
        tilesHighlights = new TileHighlight[mapData.GetLength(0), mapData.GetLength(1)];

        for (int x = 0; x < tilesHighlights.GetLength(0); x++)
        {
            for (int y = 0; y < tilesHighlights.GetLength(1); y++)
            {
                tilesHighlights[x, y] =
                    Instantiate(tileHighlightPrefab, GameWorldToMapCastController.Instance.CastMapPosToGameWorld(new MapPosition(x, y)), Quaternion.identity, this.transform).GetComponent<TileHighlight>();

                tilesHighlights[x, y].gameObject.SetActive(false);
            }
        }
    }

    public void UpdateHighlightedTiles(List<MapPosition> selectedTiles)
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