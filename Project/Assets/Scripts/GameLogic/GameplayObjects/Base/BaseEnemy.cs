using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : GameplayObject
{
    protected LootSpawner lootSpawner;
    protected EnemyTier tier;

    public BaseEnemy(Sprite gameplayImage, int attributeHealthValue, int attributeMovementSpeedValue, int attributeDamageValue, int notImmobilised, LootSpawner lootSpawner, int defence, EnemyTier tier) : base(gameplayImage)
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

    public override void ReactToSpell(BaseSpell spell, int modifierDamage, int modifierEffectLength)
    {
        int damageReceivedMinusDefence = Mathf.Clamp( (spell.Damage + modifierDamage) - attributes[AttributeID.Defence].CurrentValue, 0, int.MaxValue);
        attributes[AttributeID.Health].SubstractFromCurrentAttributeValue(damageReceivedMinusDefence);
        
        AcquireStatusesFromPlayer(spell.Statuses, modifierEffectLength);

        if(attributes[AttributeID.Health].CurrentValue <= 0)
        {
            removeFromGame = true;

            lootSpawner.TrySpawnLoot(tile);
        }

        EnemiesController.Instance.ClearDeadEnemies();
    }

    public override void ReactToAura(List<Status> statuses)
    {
        AcquireStatusesNotFromPlayer(statuses);
    }
}
