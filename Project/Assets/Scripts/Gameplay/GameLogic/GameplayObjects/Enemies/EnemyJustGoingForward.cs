using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJustGoingForward : EnemyNormal
{
    public EnemyJustGoingForward(Sprite gameplayImage, int attributeHealthValue, int attributeMovementSpeedValue, int attributeDamageValue, int notImmobilised, LootSpawner lootSpawner, int defence, EnemyTier tier) : base(gameplayImage, attributeHealthValue, attributeMovementSpeedValue, attributeDamageValue, notImmobilised, lootSpawner, defence, tier) { }

    public override void PerformActionAtEndOfPlayerTurn()
    {
    }

    public override void PerformActionAtStartOfPlayerTurn()
    {
    }
}
