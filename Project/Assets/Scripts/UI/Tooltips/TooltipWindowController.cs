using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class TooltipWindowController : MonoBehaviour
{
    [SerializeField] private RectTransform tooltipWIndow;
    [SerializeField] private TextMeshProUGUI titleMainWindow;
    [SerializeField] private TextMeshProUGUI textMainWindow;
    [SerializeField] private Transform textSpace;

    [SerializeField] private GameObject secondaryTitlePrefab;
    [SerializeField] private GameObject secondaryTextSpacePrefab;

    private List<TextMeshProUGUI> secondaryTitlesWindowPool = new List<TextMeshProUGUI>();
    private List<TextMeshProUGUI> secondaryTextsWindowPool = new List<TextMeshProUGUI>();

    private void PositionTooltipOnScreen(Vector2 tooltipPosOnScreen)
    {
        Vector2 convertedWindowPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(tooltipWIndow, tooltipPosOnScreen, Camera.main, out convertedWindowPos);
        tooltipWIndow.anchoredPosition = convertedWindowPos;
    }

    #region LoadWindow

    public void ShowTooltip(TooltipParagraph[] description, Vector2 tooltipPosOnScreen)
    {
        LoadTooltipContent(description, tooltipPosOnScreen);
    }

    private void LoadTooltipContent(TooltipParagraph[] description, Vector2 tooltipPosOnScreen)
    {
        if(!ReferenceEquals(description, null) && description.Length > 0)
        {
            tooltipWIndow.gameObject.SetActive(true);
            PositionTooltipOnScreen(tooltipPosOnScreen);

            titleMainWindow.gameObject.SetActive(true);
            titleMainWindow.text = description[0].Title;

            textMainWindow.gameObject.SetActive(true);
            textMainWindow.text = description[0].Text;

            for (int i = 1; i < description.Length; i++)
            {
                LoadSecondaryTitle(i, description);
                LoadSecondaryText(i, description);
            }
        }
    }

    private void LoadSecondaryTitle(int currentSecondaryTitleBeingLoaded, TooltipParagraph[] description)
    {
        if (currentSecondaryTitleBeingLoaded < secondaryTitlesWindowPool.Count)
        {
            if(description[currentSecondaryTitleBeingLoaded].Title != null && description[currentSecondaryTitleBeingLoaded].Title.Length > 0)
            {
                secondaryTitlesWindowPool[currentSecondaryTitleBeingLoaded].gameObject.SetActive(true);
                secondaryTitlesWindowPool[currentSecondaryTitleBeingLoaded].text = description[currentSecondaryTitleBeingLoaded].Title;
            }
        }
        else
        {
            secondaryTitlesWindowPool.Add(Instantiate(secondaryTitlePrefab, textSpace).GetComponent<TextMeshProUGUI>());
            secondaryTitlesWindowPool[currentSecondaryTitleBeingLoaded].text = description[currentSecondaryTitleBeingLoaded].Title;
        }
    }

    private void LoadSecondaryText(int currentSecondaryTextBeingLoaded, TooltipParagraph[] description)
    {

        if (currentSecondaryTextBeingLoaded < secondaryTextsWindowPool.Count)
        {
            if(!ReferenceEquals(description[currentSecondaryTextBeingLoaded].Text, null) && description[currentSecondaryTextBeingLoaded].Text.Length > 0)
            {
                secondaryTextsWindowPool[currentSecondaryTextBeingLoaded].gameObject.SetActive(true);
                secondaryTextsWindowPool[currentSecondaryTextBeingLoaded].text = description[currentSecondaryTextBeingLoaded].Text;
            }
            

            
        }
        else
        {
            if (!ReferenceEquals(description[currentSecondaryTextBeingLoaded].Text, null) && description[currentSecondaryTextBeingLoaded].Text.Length > 0)
            {
                secondaryTextsWindowPool.Add(Instantiate(secondaryTextSpacePrefab, textSpace).GetComponent<TextMeshProUGUI>());
                secondaryTextsWindowPool[currentSecondaryTextBeingLoaded].text = description[currentSecondaryTextBeingLoaded].Text;
            }
        }
    }

    #endregion

    public void ClearTooltipWindowBeforeClosing()
    {
        titleMainWindow.gameObject.SetActive(false);
        textMainWindow.gameObject.SetActive(false);

        for(int i=0; i < secondaryTitlesWindowPool.Count; i++)
        {
            secondaryTitlesWindowPool[i].gameObject.SetActive(false);
        }

        for(int i=0; i < secondaryTextsWindowPool.Count; i++)
        {
            secondaryTextsWindowPool[i].gameObject.SetActive(false);
        }

        tooltipWIndow.gameObject.SetActive(false);
    }
}
