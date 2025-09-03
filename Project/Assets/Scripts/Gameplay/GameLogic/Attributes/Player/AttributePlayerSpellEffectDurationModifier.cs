using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributePlayerSpellEffectDurationModifier : BaseAttribute
{
    public override AttributeID AttributeID => AttributeID.PlayerModifierSpellEffectDuration;


    public AttributePlayerSpellEffectDurationModifier(int defaultValue, int currentValue) : base(defaultValue, currentValue) { }
    public AttributePlayerSpellEffectDurationModifier(int defaultValue, int currentValue, int minValue) : base(defaultValue, currentValue, minValue) { }
}
