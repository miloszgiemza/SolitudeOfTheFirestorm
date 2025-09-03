using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BoundsXY
{
    public Vector2 Min => min;
    public Vector2 Max => max;

    private Vector2 min;
    private Vector2 max;

    public BoundsXY(Vector2 min, Vector2 max)
    {
        this.min = min;
        this.max = max;
    }
};

public class CastMapAndGameWorld
{    
    public MapPosition CastGameWorldPositionToPosOnMap(Vector2 cursorPosInScreenSpace, float cameraDistanceFromGameWorldPoint, float tileSizeGameworld, Vector2 posX0Y0InGameWorld, BoundsXY boundsMapGameWorld)
    {
        Vector3 cursorPosInGameWorld = Camera.main.ScreenToWorldPoint(new Vector3(cursorPosInScreenSpace.x, cursorPosInScreenSpace.y,cameraDistanceFromGameWorldPoint));

        Vector2 tilePos = new Vector2(-1, -1);

        if(cursorPosInGameWorld.x > boundsMapGameWorld.Min.x && cursorPosInGameWorld.x < boundsMapGameWorld.Max.x 
            && cursorPosInGameWorld.y > boundsMapGameWorld.Min.y && cursorPosInGameWorld.y < boundsMapGameWorld.Max.y)
        {
            tilePos = new Vector2( ( Mathf.Abs(cursorPosInGameWorld.x - posX0Y0InGameWorld.x ) / tileSizeGameworld), 
                Mathf.Abs((cursorPosInGameWorld.y -posX0Y0InGameWorld.y) / tileSizeGameworld) );

            tilePos = new Vector2(Mathf.Round(tilePos.x), Mathf.Round(tilePos.y));
        }

        return new MapPosition( (int) tilePos.x, (int) tilePos.y);
    }

    public Vector2 CastMapPositionToGameWorldPosition(MapPosition mapPosition, float gameWorldTileSize, Vector2 posX0Y0InGameWorld)
    {
        Vector2 gameWorldPosition = new Vector2(posX0Y0InGameWorld.x + mapPosition.X * gameWorldTileSize, posX0Y0InGameWorld.y + mapPosition.Y * gameWorldTileSize);

        return gameWorldPosition;
    }

    public Vector2 CastMapPositionToGameWorldPosition(int x, int y, float gameWorldTileSize, Vector2 posX0Y0InGameWorld)
    {
        Vector2 gameWorldPosition = new Vector2(posX0Y0InGameWorld.x + x * gameWorldTileSize, posX0Y0InGameWorld.y + y * gameWorldTileSize);

        return gameWorldPosition;
    }
}
