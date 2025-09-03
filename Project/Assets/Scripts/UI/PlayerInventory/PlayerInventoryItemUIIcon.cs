using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PlayerInventoryItemUIIcon : MonoBehaviour, IReturnObjectDataForTooltip
{
    private Image image;
    private Button button;

    [SerializeField] private Sprite emptySlotIcon;

    private ItemType itemType;
    private int numberInInventoryCompartment;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(DoThisOnClick);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(DoThisOnClick);
    }

    public void Initialize(ItemType itemType, int number)
    {
        this.itemType = itemType;
        numberInInventoryCompartment = number;
    }

    public void UpdateItem(Sprite icon)
    {
        if(!ReferenceEquals(icon, null))
        {
            image.sprite = icon;
        }
        else
        {
            image.sprite = emptySlotIcon;
        }
    }

    private void DoThisOnClick()
    {
        StartCoroutine(WaitTillButtonUp());
    }

    private IEnumerator WaitTillButtonUp()
    {
        yield return new WaitUntil(() => InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainAction.WasReleasedThisFrame());

        PlayerInventoryController.Instance.PlayerInventory.TryUseItem(itemType, numberInInventoryCompartment);

        PlayerInventoryUIController.Instance.HideWindow();
    }

    public TooltipParagraph[] ReturnTooltipText(GameLanguage gameLanguage)
    {
        return PlayerInventoryController.Instance.PlayerInventory.ReturnTooltipText(gameLanguage, itemType, numberInInventoryCompartment);
    }
}
