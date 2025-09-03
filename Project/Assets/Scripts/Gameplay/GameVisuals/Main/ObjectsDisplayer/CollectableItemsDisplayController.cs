using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItemsDisplayController : BaseGameplayObjectsDisplayController
{
    public static CollectableItemsDisplayController Instance => instance;
    private static CollectableItemsDisplayController instance;

    protected override SortingLayers SortingLayer => SortingLayers.CollectableItems;

    protected override void ImplementSingletion()
    {
        if(!ReferenceEquals(CollectableItemsDisplayController.Instance, null))
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    protected override void ClearSingleton()
    {
        instance = null;
    }

    public void Display(Tile[,] mapData)
    {
        for(int x = 0; x < mapData.GetLength(0); x++)
        {
            for(int y = 0; y < mapData.GetLength(1); y++)
            {
                if(ReferenceEquals(mapData[x, y].CollectableItemSocket, null))
                {
                    objectDisplayers[x, y].UnDisplay();
                }
                else
                {
                    objectDisplayers[x, y].Display(mapData[x, y].CollectableItemSocket.GameplayImage);
                }
            }
        }
    }
}
