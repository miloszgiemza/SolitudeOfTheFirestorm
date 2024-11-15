using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SpellNextTurnMiniature : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void UpdateMiniature(Sprite newSprite)
    {
        image.sprite = newSprite;
    }
}
