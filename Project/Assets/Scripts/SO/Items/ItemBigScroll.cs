using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Items/BigScroll", fileName = "NewBigScroll")]
public class ItemBigScroll : BaseItem
{
    public override ItemType ItemType => ItemType.BigScroll;

    [SerializeField] protected BaseSpell spell;

    public override void TryUseItem()
    {
        if(!ReferenceEquals(PlayerStateIdleEvents.OnCastSpellFromBigScroll, null))
        {
           SpellsController.Instance.EquipTemporarySpell(spell);
           PlayerStateIdleEvents.OnCastSpellFromBigScroll.Invoke();
        }
    }

    public override void ClearAfterItemUse()
    {
        SpellsController.Instance.UnEquipTemporarySpell();
    }

    public override TooltipParagraph[] ReturnTooltipText(GameLanguage gameLanguage)
    {
       return spell.ReturnTooltipText(gameLanguage);
    }
}
