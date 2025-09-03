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

    public override TooltipParagraph[] ReturnTooltipText(GameLanguage gameLanguage)
    {
        TooltipParagraph[] description = new TooltipParagraph[1];
        description[0] = new TooltipParagraph();

        switch (gameLanguage)
        {
            case GameLanguage.ENG:
                description[0].SetTitle(descriptionEN[0].Title);

                TooltipParagraph[] spellTooltipText = spell.ReturnTooltipText(gameLanguage);
                string spellDescriptionWithoutCost = "";
                bool firstLineSkipped = false;

                for(int i=0; i < spellTooltipText[0].Text.Length; i++)
                {
                    if(firstLineSkipped)
                    {
                        spellDescriptionWithoutCost += spellTooltipText[0].Text[i];
                    }

                    else if(spellTooltipText[0].Text[i] == '\n' && !firstLineSkipped)
                    {
                        firstLineSkipped = true;
                        i++;
                    }
                }

                description[0].SetText("Cost: SECONDARY ACTION\n\n" + spellDescriptionWithoutCost + descriptionEN[0].Text);
                break;

            case GameLanguage.PL:
                description = descriptionPL;
                break;
        }

        return description;
    }
}
