using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Items/SmallScroll", fileName = "NewSmallScroll")]
public class ItemSmallScroll : BaseItem
{
    public override ItemType ItemType => ItemType.SmallScrolls;

    [SerializeField] protected BaseSpell spell;

    public override void TryUseItem()
    {
        if (!ReferenceEquals(PlayerStateIdleEvents.OnCastSpellFromSmallScroll, null))
        {
            SpellsController.Instance.EquipTemporarySpell(spell);
           PlayerStateIdleEvents.OnCastSpellFromSmallScroll.Invoke();
        }
    }

    public override void ClearAfterItemUse()
    {
        SpellsController.Instance.UnEquipTemporarySpell();
    }
}
