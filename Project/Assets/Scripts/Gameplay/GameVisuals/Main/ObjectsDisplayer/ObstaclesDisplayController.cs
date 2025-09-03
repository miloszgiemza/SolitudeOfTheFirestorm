using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesDisplayController : BaseGameplayObjectsDisplayController
{
    protected override SortingLayers SortingLayer => SortingLayers.Obstacles;

    public static ObstaclesDisplayController Instance => instance;

    private static ObstaclesDisplayController instance;

    protected override void ImplementSingletion()
    {
        if(!ReferenceEquals(ObstaclesDisplayController.Instance, null))
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
}
