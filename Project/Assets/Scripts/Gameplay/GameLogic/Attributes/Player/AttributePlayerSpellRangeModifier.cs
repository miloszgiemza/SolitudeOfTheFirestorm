using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributePlayerSpellRangeModifier : BaseAttribute
{
    public override AttributeID AttributeID => AttributeID.PlayerModifierSpellRange;

    public AttributePlayerSpellRangeModifier(int defaultValue, int currentValue) : base(defaultValue, currentValue) { }
    public AttributePlayerSpellRangeModifier(int defaultValue, int currentValue, int minValue) : base(defaultValue, currentValue, minValue) { }
}
