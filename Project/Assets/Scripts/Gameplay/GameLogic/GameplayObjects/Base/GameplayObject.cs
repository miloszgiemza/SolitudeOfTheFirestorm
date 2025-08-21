using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public enum GameplayObjectType
{
    Enemy,
    EnemyFlying,
    EnemyRanged,
    EnemyWithAreaAura,
    Obstacle,
    CollectableItem
}

[Serializable]
public abstract class GameplayObject : IComparable, IReturnObjectDataForTooltip
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

    protected TooltipParagraph[] descriptionEN;
    protected TooltipParagraph[] descriptionPL;

    public GameplayObject(Sprite gameplayImage, TooltipParagraph[] descriptionEN, TooltipParagraph[] descriptionPL)
    {
        this.gameplayImage = gameplayImage;

        this.descriptionEN = descriptionEN;
        this.descriptionPL = descriptionPL;
    }

    public abstract void ReactToSpell(BaseSpell spell, int spellDamage, int modifierDamage, int modifierEffectLength);
    public abstract void PerformEnemyTurnAction();
    public abstract void PerformEnemyTurnAction(int enemyMaxSpeed);
    public abstract void PerformActionAtEndOfPlayerTurn();
    public abstract void PerformActionAtStartOfPlayerTurn();
    public abstract void ReactToAura(List<Status> statuses);

    public virtual int ReturnReceivedDamage(int spellDamage, int modifierDamage)
    {
        return 0;
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

    public virtual TooltipParagraph[] ReturnTooltipText(GameLanguage gameLanguage)
    {
        TooltipParagraph[] description = new TooltipParagraph[0];

        switch (gameLanguage)
        {
            case GameLanguage.ENG:
                description = descriptionEN;
                break;

            case GameLanguage.PL:
                description = descriptionPL;
                break;
        }

        return description;
    }

    public void MarkToRemoveFromGame()
    {
        removeFromGame = true;
    }
}
