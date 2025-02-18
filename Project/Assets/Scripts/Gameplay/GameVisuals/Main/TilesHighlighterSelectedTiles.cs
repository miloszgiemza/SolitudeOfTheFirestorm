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
}
