using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SortingLayers
{
    Enemies,
    Obstacles,
    CollectableItems
}

public abstract class BaseGameplayObjectsDisplayController : MonoBehaviour
{
    protected abstract SortingLayers SortingLayer { get; }

    [SerializeField] protected GameObject gameplayObjectVisual;

    protected GameplayObjectDisplayer[,] objectDisplayers;

    protected void Awake()
    {
        ImplementSingletion();
    }

    protected abstract void ImplementSingletion();
    protected abstract void ClearSingleton();

    protected void Start()
    {
        CreateObjectDisplayersForCurrentMap(Map.Instance.MapData);
    }

    protected void OnDisable()
    {
        ClearSingleton();
    }

    protected void OnDestroy()
    {
        ClearSingleton();
    }

    protected void CreateObjectDisplayersForCurrentMap(Tile[,] mapData)
    {
        objectDisplayers = new GameplayObjectDisplayer[mapData.GetLength(0), mapData.GetLength(1)];

        for (int x = 0; x < objectDisplayers.GetLength(0); x++)
        {
            for (int y = 0; y < objectDisplayers.GetLength(1); y++)
            {
                objectDisplayers[x, y] = Instantiate(gameplayObjectVisual, new Vector3(GameWorldToMapCastController.Instance.CastMapPosToGameWorld(new MapPosition(x, y)).x, 
                    GameWorldToMapCastController.Instance.CastMapPosToGameWorld(new MapPosition(x, y)).y, 0f), Quaternion.identity, this.transform).GetComponent<GameplayObjectDisplayer>();
                objectDisplayers[x, y].SetSortingLayer(SortingLayer.ToString());
            }
        }
    }

    protected void ResetDisplaydObjects()
    {
        for (int x = 0; x < objectDisplayers.GetLength(0); x++)
        {
            for (int y = 0; y < objectDisplayers.GetLength(1); y++)
            {
                objectDisplayers[x, y].UnDisplay();
            }
        }
    }

    public virtual void Display(List<GameplayObject> gameplayObject)
    {
        ResetDisplaydObjects();

        for (int i = 0; i < gameplayObject.Count; i++)
        {
            objectDisplayers[gameplayObject[i].Tile.Position.X, gameplayObject[i].Tile.Position.Y].Display(gameplayObject[i].GameplayImage);
        }
    }
}
