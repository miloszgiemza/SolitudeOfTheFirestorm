using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayObjectDisplayer : MonoBehaviour
{ 
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSortingLayer(string sortingLayer)
    {
        spriteRenderer.name = sortingLayer;
    }

    public void Display(Sprite imageToDisplay)
    {
        spriteRenderer.sprite = imageToDisplay;
    }

    public void UnDisplay()
    {
        spriteRenderer.sprite = null;
    }
}
