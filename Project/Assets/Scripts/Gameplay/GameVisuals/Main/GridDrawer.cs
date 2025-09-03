using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDrawer : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private Vector2[,] gridPoints;

    private BoundsXY boundsXY;

    private float gridWidth;
    private float gridHeight;

    private float gridTileWidth;
    private float gridTileHeight;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        StartCoroutine(WaitTillGameWorldCasterInitialised());
    }

    private void DrawGrid(Tile[,] mapData)
    {
        boundsXY = GameWorldToMapCastController.Instance.MapGameWorldBounds;
        gridWidth = Mathf.Abs(boundsXY.Max.x - boundsXY.Min.x);
        gridHeight = Mathf.Abs(boundsXY.Max.y - boundsXY.Min.y);

        gridTileWidth = gridWidth / (float) mapData.GetLength(0);
        gridTileHeight = gridHeight / (float) mapData.GetLength(1);

        if (gridTileHeight % 2 == 0)
        {
            lineRenderer.positionCount = ((mapData.GetLength(1) + 1) * 2 + (mapData.GetLength(0) + 1) * 2);
        }
        else
        {
            lineRenderer.positionCount = ((mapData.GetLength(1) + 1) * 2 + (mapData.GetLength(0) + 1) * 2) - 1;
        }

        int index = -1;

        for(int y = 0; y < (mapData.GetLength(1) + 1); y++)
        {
            if (y % 2 == 0)
            {
                index++;
                lineRenderer.SetPosition(index, new Vector2(boundsXY.Min.x, boundsXY.Min.y + y * gridTileHeight) );
                index++;
                lineRenderer.SetPosition(index, new Vector2(boundsXY.Max.x, boundsXY.Min.y + y * gridTileHeight));
            }
            else
            {
                index++;
                lineRenderer.SetPosition(index, new Vector2(boundsXY.Max.x, boundsXY.Min.y + y * gridTileHeight));
                index++;
                lineRenderer.SetPosition(index, new Vector2(boundsXY.Min.x, boundsXY.Min.y + y * gridTileHeight));
            }
        }

        if (gridTileHeight % 2 == 0)
        {
            index++;
            lineRenderer.SetPosition(index, new Vector2(boundsXY.Min.x, boundsXY.Max.y));
        }


            for (int x = 0; x < (mapData.GetLength(0) + 1); x++)
        {
            if (x % 2 == 0)
            {
                index++;
                lineRenderer.SetPosition(index, new Vector2(boundsXY.Min.x + x * gridTileWidth, boundsXY.Min.y));
                index++;
                if(x < mapData.GetLength(0))lineRenderer.SetPosition(index, new Vector2(boundsXY.Min.x + x * gridTileWidth + gridTileWidth, boundsXY.Min.y));
            }
            else
            {
                index++;
                lineRenderer.SetPosition(index, new Vector2(boundsXY.Min.x + x * gridTileWidth, boundsXY.Max.y));
                index++;
                if (x < mapData.GetLength(0)) lineRenderer.SetPosition(index, new Vector2(boundsXY.Min.x + x * gridTileWidth + gridTileWidth, boundsXY.Max.y));
            }
        }
    }

    private IEnumerator WaitTillGameWorldCasterInitialised()
    {
        yield return new WaitUntil(() => GameWorldToMapCastController.Instance.Initialised);
        DrawGrid(Map.Instance.MapData);
    }
}


