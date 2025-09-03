using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeNotImmobilised : BaseAttribute
{
    public override AttributeID AttributeID => AttributeID.NotImmobilised;

    public AttributeNotImmobilised(int defaultValue, int currentValue) : base(defaultValue, currentValue) { }
    public AttributeNotImmobilised(int defaultValue, int currentValue, int minValue) : base(defaultValue, currentValue, minValue) { }
}
