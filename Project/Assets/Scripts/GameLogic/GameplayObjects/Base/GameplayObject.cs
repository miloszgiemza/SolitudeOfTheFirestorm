using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public enum GameplayObjectType
{
    Enemy,
    EnemyFlying,
    EnemyWithAreaAura,
    Obstacle,
    CollectableItem
}

[Serializable]
public abstract class GameplayObject : IComparable
{
    public abstract GameplayObjectType GameplayObjectType {get;}

    public bool RemoveFromGame => removeFromGame;
    public Sprite GameplayImage => gameplayImage;
    public Dictionary<AttributeID, BaseAttribute> Attributes => attributes;
    public Tile Tile => tile;
    public MapPosition[] MovementDirections => movementDirections;


    protected bool removeFromGame = false;
    protected Sprite gameplayImage;

    protected Dictionary<AttributeID, BaseAttribute> attributes = new Dictionary<AttributeID, BaseAttribute>();
    
    protected Tile tile;

    protected MapPosition[] movementDirections = { new MapPosition(0, -1) };

    public abstract void ReactToSpell(BaseSpell spell, int modifierDamage, int modifierEffectLength);
    public abstract void PerformEnemyTurnAction();
    public abstract void PerformActionAtEndOfPlayerTurn();
    public abstract void PerformActionAtStartOfPlayerTurn();
    public abstract void ReactToAura(List<Status> statuses);

    public GameplayObject(Sprite gameplayImage)
    {
        this.gameplayImage = gameplayImage;
    }


    public void SetValues(Tile tile)
    {
        this.tile = tile;
    }

    public void SetValues(Tile tile, int health)
    {
        this.tile = tile;
        attributes[AttributeID.Health].SetCurrentAttributeValue(health);
    }

    #region StatusesAndAttributes
    protected void AcquireStatusesFromPlayer(List<Status> newStatuses, int modifierPlayerSpellDuration)
    {
        for (int currentNewStatus = 0; currentNewStatus < newStatuses.Count; currentNewStatus++)
        {
            attributes[newStatuses[currentNewStatus].Attribute].AddStatus(new Status(newStatuses[currentNewStatus].Attribute, newStatuses[currentNewStatus].Duration + modifierPlayerSpellDuration, newStatuses[currentNewStatus].Modifier));
        }
    }

    protected void AcquireStatusesNotFromPlayer(List<Status> newStatuses)
    {
        for (int currentNewStatus = 0; currentNewStatus < newStatuses.Count; currentNewStatus++)
        {
            attributes[newStatuses[currentNewStatus].Attribute].AddStatus(new Status(newStatuses[currentNewStatus].Attribute, newStatuses[currentNewStatus].Duration, newStatuses[currentNewStatus].Modifier));
        }
    }

    public void ApplyStatuses()
    {
        foreach(KeyValuePair<AttributeID, BaseAttribute> attribute in attributes)
        {
            attribute.Value.ApplyModifiersAtStartOfTurn();
        }
    }

    #endregion

    public int CompareTo(object obj)
    {
        GameplayObject arg = (GameplayObject)obj;

        if (tile.Position.Y < arg.tile.Position.Y)
            return -1;
        else if (tile.Position.Y == arg.tile.Position.Y)
            return 0;
        else return 1;
    }
}