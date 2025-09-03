using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItemsController : MonoBehaviour
{
    public static DroppedItemsController Instance => instance;
    private static DroppedItemsController instance;

    [SerializeField] private int maxDroppedItemsCount = 5;

    private BaseItem[] droppedItems;

    private void Awake()
    {
        if(!ReferenceEquals(DroppedItemsController.Instance, null))
        {
            Destroy(this);
        }
        else
        {
            instance = this;

            Initialize(maxDroppedItemsCount);
        }
    }

    private void Initialize(int maxDroppedItemsCount)
    {
        droppedItems = new BaseItem[maxDroppedItemsCount];
    }

    public void ReceiveDroppedItem(BaseItem newDroppedItem)
    {
        bool emptySpotFound = false;

        for(int i=0; i < droppedItems.Length && emptySpotFound==false; i++)
        {
            if(droppedItems[i]==null)
            {
                droppedItems[i] = newDroppedItem;
                emptySpotFound=true;

                Debug.Log("Item dropped");
            }
        }

        if(emptySpotFound==false)
        {
            droppedItems[0] = newDroppedItem;

            Debug.Log("Item dropped");
        }

        DroppedItemsUIController.Instance.UpdateButtons(ReturnCurrentDroppedItemsCount(droppedItems), ReturnCurrentDroppedItemsSprites(droppedItems));
    }

    public BaseItem ReturnItemToPlayerInventory(int itemNumber)
    {
        BaseItem itemToReturn = droppedItems[itemNumber];
        droppedItems[itemNumber] = null;

        ReorganiseDroppedItemsArray(droppedItems);

        DroppedItemsUIController.Instance.UpdateButtons(ReturnCurrentDroppedItemsCount(droppedItems), ReturnCurrentDroppedItemsSprites(droppedItems));

        return itemToReturn;
    }

    private void ReorganiseDroppedItemsArray(BaseItem[] droppedItems)
    {
        for(int i=0; i < droppedItems.Length; i++)
        {
            if(ReferenceEquals(droppedItems[i], null))
            {
                for(int j=i+1; j < droppedItems.Length; j++)
                {
                    if(!ReferenceEquals(droppedItems[j], null))
                    {
                        droppedItems[i] = droppedItems[j];
                        droppedItems[j] = null;
                    }
                }
            }
        }
    }

    private int ReturnCurrentDroppedItemsCount(BaseItem[] droppedItems)
    {
        int currentDroppedItemsCount = 0;

        for(int i=0; i < droppedItems.Length; i++)
        {
            if(!ReferenceEquals(droppedItems[i], null))
            {
                currentDroppedItemsCount++;
            }
        }

        return currentDroppedItemsCount;
    }
    private List<Sprite> ReturnCurrentDroppedItemsSprites(BaseItem[] droppedItems)
    {
        List<Sprite> droppedItemsIcons = new List<Sprite>();

        for (int i = 0; i < droppedItems.Length; i++)
        {
            if (!ReferenceEquals(droppedItems[i], null))
            {
                droppedItemsIcons.Add(droppedItems[i].Icon);
            }
        }

        return droppedItemsIcons;
    }

    public TooltipParagraph[] ReturnTooltipText(GameLanguage gameLanguage, int item)
    {
        return droppedItems[item].ReturnTooltipText(gameLanguage);
    }
}
