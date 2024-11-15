using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : GameplayObject
{
    public override GameplayObjectType GameplayObjectType => GameplayObjectType.Obstacle;

    public int Duration => duration;
    public bool Walkable => walkable;
    public List<Status> Statuses => statuses;

    protected int duration;
    protected bool walkable = false;
    protected List<Status> statuses = new List<Status>();

    public Obstacle(Sprite gameplayImage, bool walkable, int attributeDamageValue, int duration, List<Status> statuses, Tile tile) : 
        base(gameplayImage) 
    {
        attributes.Add(AttributeID.Damage, new AttributeDamage(attributeDamageValue, attributeDamageValue));
        this.duration = duration;
        this.walkable = walkable;
        this.tile = tile;

        foreach(Status status in statuses)
        {
            this.statuses.Add(new Status(status.Attribute, status.Duration, status.Modifier));
        }
    }

    public override void PerformEnemyTurnAction() {}

    public override void PerformActionAtStartOfPlayerTurn()
    {
        duration--;

        if(duration <= 0)
        {
            removeFromGame = true;
        }
    }

    public override void ReactToSpell(BaseSpell spell, int modifierDamage, int modifierEffectLength)
    {
    }

    public override void ReactToAura(List<Status> statuses)
    {
    }

    public override void PerformActionAtEndOfPlayerTurn()
    {
    }
}
