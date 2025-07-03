using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesHighlighterSelectedTiles : BaseTilesHighlighter
{
    public static TilesHighlighterSelectedTiles Instance => instance;
    private static TilesHighlighterSelectedTiles instance;
    protected override void MakeASingleton()
    {
        if(!ReferenceEquals(TilesHighlighterSelectedTiles.Instance, null))
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    protected override void UnmakeSingleton()
    {
        instance = null;
    }

    protected override void CreateTilesHighlights(Tile[,] mapData)
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

    public override void UpdateHighlightedTiles(List<MapPosition> selectedTiles)
    {
        if(!CheckIfObscuredByUI.Instance.CheckIfObscured())
        {
            for (int i = 0; i < selectedTiles.Count; i++)
            {
                if (!tilesHighlights[selectedTiles[i].X, selectedTiles[i].Y].gameObject.activeSelf)
                {
                    tilesHighlights[selectedTiles[i].X, selectedTiles[i].Y].gameObject.SetActive(true);

                    TilesUIDisplayController.Instance.TilesUIDisplayers[selectedTiles[i].X, selectedTiles[i].Y].UpdatePotentialDamageValue(
                         Map.Instance.MapData[selectedTiles[i].X, selectedTiles[i].Y].ReturnObjectReceivedDamage(
                             SpellsController.Instance.CurrentSpell.ReturnDamage(Map.Instance.MapData[selectedTiles[i].X, selectedTiles[i].Y], Map.Instance.MapData,
                             InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>(), Player.Instance.Attributes[AttributeID.PlayerModifierSpellRange].CurrentValue),
                        Player.Instance.Attributes[AttributeID.PlayerModifierSpellDamage].CurrentValue).ToString());
                }

                tilesHighlights[selectedTiles[i].X, selectedTiles[i].Y].RefreshLifetime();
            }
        }
    }
}
