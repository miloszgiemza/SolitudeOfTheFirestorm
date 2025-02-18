using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItemsUIController : BaseDynamicallyGeneratedButtonsController
{
    public static DroppedItemsUIController Instance => instance;
    private static DroppedItemsUIController instance;

    private new List<ButtonDroppedItem> buttons = new List<ButtonDroppedItem>();

    private void Awake()
    {
        if(!ReferenceEquals(DroppedItemsUIController.Instance, null))
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void OnDestroy()
    {
        instance = null;
    }

    protected void CreateButton(int buttonNumber, Sprite buttonIcon)
    {
        ButtonDroppedItem newButton = Instantiate(buttonsPrefab, buttonsParent.transform).GetComponent<ButtonDroppedItem>();
        newButton.Initialize(buttonNumber, buttonIcon);
        buttons.Add(newButton);
    }

    public void UpdateButtons(int currentButtonsCount, List<Sprite> buttonsSprites)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < currentButtonsCount; i++)
        {
            if (i < buttons.Count)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].Initialize(i, buttonsSprites[i]);
            }
            else
            {
                CreateButton(i, buttonsSprites[i]);
            }
        }
    }
}
