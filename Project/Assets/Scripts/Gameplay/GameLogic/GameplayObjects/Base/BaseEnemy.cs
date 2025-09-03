using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : GameplayObject
{
    protected LootSpawner lootSpawner;
    protected EnemyTier tier;

    public BaseEnemy(Sprite gameplayImage, TooltipParagraph[] descriptionEN, TooltipParagraph[] descriptionPL,
        int attributeHealthValue, int attributeMovementSpeedValue, int attributeDamageValue, int notImmobilised, LootSpawner lootSpawner, int defence, EnemyTier tier) 
        : base(gameplayImage, descriptionEN, descriptionPL)
    {
        attributes.Add(AttributeID.Health, new AttributeHealth(attributeHealthValue, attributeHealthValue));
        attributes.Add(AttributeID.MovementSpeed, new AttributeMovementSpeed(attributeMovementSpeedValue, attributeMovementSpeedValue, 1));
        attributes.Add(AttributeID.Damage, new AttributeDamage(attributeDamageValue, attributeDamageValue));
        attributes.Add(AttributeID.NotImmobilised, new AttributeNotImmobilised(notImmobilised, notImmobilised));
        attributes.Add(AttributeID.Defence, new AttributeDefence(defence, defence));

        this.lootSpawner = lootSpawner;
        this.tier = tier;
    }

    protected abstract void PerformInteractionsWithOtherObjectsOnTileOnEnteringTile(Tile tile);

    public override int ReturnReceivedDamage(int spellDamage, int modifierDamage)
    {
        return Mathf.Clamp((spellDamage + modifierDamage) - attributes[AttributeID.Defence].CurrentValue, 0, int.MaxValue);
    }

    public override void ReactToSpell(BaseSpell spell, int spellDamage, int modifierDamage, int modifierEffectLength)
    {
        int damageReceivedMinusDefence = ReturnReceivedDamage(spellDamage, modifierDamage);
        //int damageReceivedMinusDefence = Mathf.Clamp( (spellDamage + modifierDamage) - attributes[AttributeID.Defence].CurrentValue, 0, int.MaxValue);
        attributes[AttributeID.Health].SubstractFromCurrentAttributeValue(damageReceivedMinusDefence);
        
        AcquireStatusesFromPlayer(spell.Statuses, modifierEffectLength);

        if(attributes[AttributeID.Health].CurrentValue <= 0)
        {
            removeFromGame = true;

            //lootSpawner.TrySpawnLootTile(tile);
            lootSpawner.TrySpawnLootUI();
        }

        EnemiesController.Instance.ClearDeadEnemies();
    }

    public override void ReactToAura(List<Status> statuses)
    {
        AcquireStatusesNotFromPlayer(statuses);
    }

    protected void DealDamageToPlayer()
    {
        Player.Instance.ReceiveDamage(attributes[AttributeID.Damage].CurrentValue);
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


                foreach(var item in attributes)
                {
                    stats = stats + item.Key.ToString() + ": " + item.Value.DefaultValue.ToString() + " (" + item.Value.CurrentValue.ToString() + ")\n";  
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
