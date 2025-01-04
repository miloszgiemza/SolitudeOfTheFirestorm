using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeDefence : BaseAttribute
{
    public override AttributeID AttributeID => AttributeID.Defence;

    public AttributeDefence(int defaultValue, int currentValue) : base(defaultValue, currentValue)
    {
    }
}
