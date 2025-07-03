using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : GameplayObject
{
    public override GameplayObjectType GameplayObjectType => GameplayObjectType.Obstacle;

    public int Duration => duration;
    public bool Walkable => walkable;
    public float MovementCostReal => movementCostReal;
    public float MovementCostHeuristic => movementCostHeuristic;
    public List<Status> Statuses => statuses;

    protected int duration;
    protected bool walkable = false;
    protected float movementCostReal = 0f;
    protected float movementCostHeuristic = 0f;
    protected List<Status> statuses = new List<Status>();

    public Obstacle(Sprite gameplayImage, TooltipParagraph[] descriptionEN, TooltipParagraph[] descriptionPL,
        bool walkable, int attributeDamageValue, int duration, List<Status> statuses, Tile tile, float movementCostReal, float movementCostHeuristic) : 
        base(gameplayImage, descriptionEN, descriptionPL) 
    {
        attributes.Add(AttributeID.Damage, new AttributeDamage(attributeDamageValue, attributeDamageValue));
        this.duration = duration;
        this.walkable = walkable;
        this.tile = tile;
        this.movementCostReal = movementCostReal;
        this.movementCostHeuristic = movementCostHeuristic;

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

    public override void ReactToSpell(BaseSpell spell,  int spellDamage, int modifierDamage, int modifierEffectLength)
    {
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
