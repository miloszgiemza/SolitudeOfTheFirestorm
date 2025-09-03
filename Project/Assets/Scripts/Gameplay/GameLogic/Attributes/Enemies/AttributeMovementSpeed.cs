using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeMovementSpeed : BaseAttribute
{
    public override AttributeID AttributeID => AttributeID.MovementSpeed;

    public AttributeMovementSpeed(int defaultValue, int currentValue) : base(defaultValue, currentValue) { }
    public AttributeMovementSpeed(int defaultValue, int currentValue, int minValue) : base(defaultValue, currentValue, minValue) { }
}
