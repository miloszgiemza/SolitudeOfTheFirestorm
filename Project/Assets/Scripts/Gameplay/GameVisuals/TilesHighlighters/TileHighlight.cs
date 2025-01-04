using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHighlight : MonoBehaviour
{
    private float lifetime = 0.1f;

    private float currenLifetime = 0f;

    private void OnEnable()
    {
        currenLifetime = lifetime;
    }

    private void Update()
    {
        currenLifetime -= Time.deltaTime;

        if(currenLifetime <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void RefreshLifetime()
    {
        currenLifetime = lifetime;
    }
}
