using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class TileHighlight : MonoBehaviour
{
    private float lifetime = 0.1f;

    private float currenLifetime = 0f;

    private MapPosition mapPosition;

    private void OnEnable()
    {
        currenLifetime = lifetime;
    }

    private void Update()
    {
        currenLifetime -= Time.deltaTime;

        if(currenLifetime <= 0)
        {
            TilesUIDisplayController.Instance.TilesUIDisplayers[mapPosition.X, mapPosition.Y].HidePotentialDamageValue();
            this.gameObject.SetActive(false);
        }
    }

    public void Construct(int x, int y)
    {
        mapPosition = new MapPosition(x, y);
    }

    public void RefreshLifetime()
    {
        currenLifetime = lifetime;
    }

    public void Clear()
    {
        TilesUIDisplayController.Instance.TilesUIDisplayers[mapPosition.X, mapPosition.Y].HidePotentialDamageValue();
    }
}
