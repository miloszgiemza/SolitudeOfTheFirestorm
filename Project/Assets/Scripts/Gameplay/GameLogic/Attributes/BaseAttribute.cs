using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttributeID
{
    Health,
    MovementSpeed,
    Damage,
    NotImmobilised,
    Defence,

    PlayerModifierSpellRange,
    PlayerModifierSpellDamage,
    PlayerModifierSpellEffectDuration
}

public abstract class BaseAttribute
{
    public abstract AttributeID AttributeID { get; }

    public int DefaultValue => defaultValue;
    public int CurrentValue => currentValue;

    protected int defaultValue;
    protected int currentValue;
    protected int minValue;
    protected List<Status> statuses = new List<Status>();

    public BaseAttribute(int defaultValue, int currentValue)
    {
        this.defaultValue = defaultValue;
        this.currentValue = currentValue;
        minValue = 0;
    }

    public BaseAttribute(int defaultValue, int currentValue, int minValue)
    {
        this.defaultValue = defaultValue;
        this.currentValue = currentValue;
        this.minValue = minValue;
    }

    public void SetCurrentAttributeValue(int newValue)
    {
        Mathf.Clamp(newValue, minValue, Mathf.Infinity);
    }

    public void AddToCurrentAttributeValue(int addThis)
    {
        Mathf.Clamp(currentValue += addThis, minValue, Mathf.Infinity);
    }

    public void SubstractFromCurrentAttributeValue(int substractThis)
    {
        Mathf.Clamp(currentValue -= substractThis, minValue, Mathf.Infinity);
    }

    public void AddStatus(Status newStatus)
    {
        statuses.Add(new Status(newStatus.Attribute, newStatus.Duration, newStatus.Modifier));
        
    }

    public int ReturnAttributeValue()
    {
        return currentValue;
    }

    /*
    public int ReturnAttributeWithModifiers()
    {
        int modifirsSummedUp = 0;

        for(int i=0; i < statuses.Count; i++)
        {
            modifirsSummedUp += statuses[i].Modifier;
        }

        return ((int) Mathf.Clamp(currentValue + modifirsSummedUp, minValue, Mathf.Infinity));
    }
    */

    public virtual void ApplyModifiersAtStartOfTurn()
    {
        currentValue = defaultValue;

        int modifirsSummedUp = 0;

        for (int i = 0; i < statuses.Count; i++)
        {
            if(statuses[i].Duration > 0)
            {
                modifirsSummedUp += statuses[i].Modifier;
                statuses[i].DecreaseDuration();
            }
            else
            {
                statuses.RemoveAt(i);
            }
        }

        currentValue = (int)Mathf.Clamp(currentValue + modifirsSummedUp, minValue, Mathf.Infinity);
    }

    public void CheckAndClearStatuses()
    {
        for (int i = 0; i < statuses.Count; i++)
        {
            statuses[i].DecreaseDuration();
            
            if(statuses[i].Duration <= 0)
            {
                statuses.RemoveAt(i);
            }
        }
    }
}
