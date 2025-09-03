using UnityEngine;

namespace DebugMode
{
    public class TileSelector : MonoBehaviour
    {
        public Tile ReturnTile(Vector2 cursorPos, Tile[,] mapData)
        {
            Tile selectedTile = null;

            MapPosition mapPosition = GameWorldToMapCastController.Instance.CastGameWorldPosToMap(cursorPos);

            if (mapPosition.X >= 0 && mapPosition.X < mapData.GetLength(0) && mapPosition.Y >= 0 && mapPosition.Y < mapData.GetLength(1))
            {
                selectedTile = mapData[mapPosition.X, mapPosition.Y];
            }

            return selectedTile;
        }
    }
}
