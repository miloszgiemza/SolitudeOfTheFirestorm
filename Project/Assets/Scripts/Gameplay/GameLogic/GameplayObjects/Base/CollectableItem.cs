using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : GameplayObject
{
    public override GameplayObjectType GameplayObjectType => GameplayObjectType.CollectableItem;

    protected BaseItem item;

    public CollectableItem(Sprite gameplayImage, TooltipParagraph[] descriptionEN, TooltipParagraph[] descriptionPL, Tile tile, BaseItem item) : base(gameplayImage, descriptionEN, descriptionPL)
    {
        //musi dodawaæ siebie kafelkowi i sobie kafelek
        this.tile = tile;
        this.tile.UpdateTile(this);

        this.item = item;

        CollectableItemsDisplayController.Instance.Display(Map.Instance.MapData);
    }

    public override void PerformActionAtStartOfPlayerTurn()
    {
    }

    public override void PerformEnemyTurnAction()
    {
    }

    public override void ReactToSpell(BaseSpell spell,  int spellDamage, int modifierDamage, int modifierEffectLength)
    {
        PlayerInventoryController.Instance.PlayerInventory.ReceiveItemAndTryToAcquireIt(item);
        tile.Clear(this);
        CollectableItemsDisplayController.Instance.Display(Map.Instance.MapData);
    }

    public override void ReactToAura(List<Status> statuses)
    {
    }

    public override void PerformActionAtEndOfPlayerTurn()
    {
    }

    public override void PerformEnemyTurnAction(int enemyMaxSpeed)
    {
        throw new System.NotImplementedException();
    }
}
