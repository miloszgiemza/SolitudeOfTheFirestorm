using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesHighlighterTilesInRange : BaseTilesHighlighter
{
    public static TilesHighlighterTilesInRange Instance => instance;
    private static TilesHighlighterTilesInRange instance;

    protected override void MakeASingleton()
    {
        if(!ReferenceEquals(TilesHighlighterTilesInRange.Instance, null))
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
}
