using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDynamicallyGeneratedButtonsController : MonoBehaviour
{
    [SerializeField] protected GameObject buttonsPrefab;
    [SerializeField] protected GameObject buttonsParent;

    protected List<BaseDynamicallyGeneratedButton> buttons = new List<BaseDynamicallyGeneratedButton>();

    protected virtual void CreateButton(int buttonNumber, Sprite buttonIcon, Action<int> buttonMethod)
    {
        BaseDynamicallyGeneratedButton newButton = Instantiate(buttonsPrefab, buttonsParent.transform).GetComponent<BaseDynamicallyGeneratedButton>();
        newButton.Initialize(buttonNumber, buttonIcon, buttonMethod);
        buttons.Add(newButton);
    }

    public virtual void UpdateButtons(int currentButtonsCount, List<Sprite> buttonsSprites, Action<int> buttonsMethod)
    {
        for(int i=0; i < buttons.Count; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }

        for(int i=0; i < currentButtonsCount; i++)
        {
            if(i < buttons.Count)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].Initialize(i, buttonsSprites[i], buttonsMethod);
            }
            else
            {
                CreateButton(i, buttonsSprites[i], buttonsMethod);
            }
        }
    }
}
