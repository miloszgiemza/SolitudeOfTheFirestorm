using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTryingToBypassObstacle : EnemyNormal
{
    protected new MapPosition[] movementDirections = { new MapPosition(0, -1), new MapPosition(1, -1), new MapPosition(-1, -1),  new MapPosition(1, 0), new MapPosition(-1, 0)};

    public EnemyTryingToBypassObstacle(Sprite gameplayImage, int attributeHealthValue, int attributeMovementSpeedValue, int attributeDamageValue, int notImmobilised, LootSpawner lootSpawner, int defence, EnemyTier tier) : base(gameplayImage, attributeHealthValue, attributeMovementSpeedValue, attributeDamageValue, notImmobilised, lootSpawner, defence, tier) { }

    public override void PerformActionAtEndOfPlayerTurn()
    {
    }

    public override void PerformActionAtStartOfPlayerTurn()
    {
    }
}
