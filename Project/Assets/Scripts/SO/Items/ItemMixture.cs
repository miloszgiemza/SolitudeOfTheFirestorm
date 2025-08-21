using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Items/Mixture", fileName = "NewMixture")]
public class ItemMixture : BaseItem
{
    public override ItemType ItemType => ItemType.Mixture;

    public List<Status> Statuses => statuses;

    [SerializeField] protected List<Status> statuses = new List<Status>();

    public override void TryUseItem()
    {
        if(!ReferenceEquals(PlayerStateIdleEvents.OnTryDrinkMixture, null))
        {
            PlayerStateIdleEvents.OnTryDrinkMixture.Invoke();
            if(!ReferenceEquals(PlayerStateDrinkMixtureEvents.OnDrinMixture, null))PlayerStateDrinkMixtureEvents.OnDrinMixture.Invoke(this);
        }
    }

    public override void ClearAfterItemUse()
    {
    }

    public override TooltipParagraph[] ReturnTooltipText(GameLanguage gameLanguage)
    {
        TooltipParagraph[] description = new TooltipParagraph[1];
        description[0] = new TooltipParagraph();

        string stats = "";

        switch (gameLanguage)
        {
            case GameLanguage.ENG:
                description[0].SetTitle(descriptionEN[0].Title);

                stats += "Cost:  SECONDARY ACTION\n\n";

                stats += "Statuses: ";

                if (ReferenceEquals(statuses, null) || statuses.Count < 1)
                {
                    stats += "None\n\n";
                }
                else
                {
                    stats += "\n\n";

                    for (int i = 0; i < statuses.Count; i++)
                    {
                        stats += "Affected attribute: " + statuses[i].Attribute.ToString() + "\n";
                        stats += "Applied modifier: +" + statuses[i].Modifier.ToString() + "\n";
                        stats += "Duration (turns): " + statuses[i].Duration.ToString() + "\n\n";
                    }
                }

                description[0].SetText(stats + "\n\n" + descriptionEN[0].Text);
                break;

            case GameLanguage.PL:
                description = descriptionPL;
                break;
        }

        return description;
    }
}
