using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeHealth : BaseAttribute
{
    public override AttributeID AttributeID => AttributeID.Health;

    public AttributeHealth(int defaultValue, int currentValue) : base(defaultValue, currentValue) { }
    public AttributeHealth(int defaultValue, int currentValue, int minValue) : base(defaultValue, currentValue, minValue) { }

    public override void ApplyModifiersAtStartOfTurn()
    {
        int modifirsSummedUp = 0;

        for (int i = 0; i < statuses.Count; i++)
        {
            if (statuses[i].Duration > 0)
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
}
