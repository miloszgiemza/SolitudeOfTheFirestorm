using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributePlayerSpellDamageModifier : BaseAttribute
{
    public override AttributeID AttributeID => AttributeID.PlayerModifierSpellDamage;

    public AttributePlayerSpellDamageModifier(int defaultValue, int currentValue) : base(defaultValue, currentValue) { }
    public AttributePlayerSpellDamageModifier(int defaultValue, int currentValue, int minValue) : base(defaultValue, currentValue, minValue) { }
}
