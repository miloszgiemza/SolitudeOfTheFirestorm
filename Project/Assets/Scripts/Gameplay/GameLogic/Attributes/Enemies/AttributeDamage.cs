using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeDamage : BaseAttribute
{
    public override AttributeID AttributeID => AttributeID.Damage;

    public AttributeDamage(int defaultValue, int currentValue) : base(defaultValue, currentValue) { }
    public AttributeDamage(int defaultValue, int currentValue, int minValue) : base(defaultValue, currentValue, minValue) { }
}
