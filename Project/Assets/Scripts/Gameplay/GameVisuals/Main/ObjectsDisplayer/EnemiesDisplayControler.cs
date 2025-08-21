using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesDisplayControler : BaseGameplayObjectsDisplayController
{
    protected override SortingLayers SortingLayer => SortingLayers.Enemies;

    public static EnemiesDisplayControler Instance => instance;

    private static EnemiesDisplayControler instance;

    protected override void ImplementSingletion()
    {
        if (!ReferenceEquals(EnemiesDisplayControler.Instance, null))
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
