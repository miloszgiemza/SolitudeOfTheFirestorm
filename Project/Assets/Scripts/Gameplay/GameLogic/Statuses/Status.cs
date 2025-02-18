using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[Serializable]
public class Status
{
    public AttributeID Attribute => attribute;
    public int Duration => duration;
    public int Modifier => modifier;

    [SerializeField] private AttributeID attribute;
    [SerializeField] private int duration;
    [SerializeField] private int modifier;

    public Status(AttributeID attribute, int duration, int modifier)
    {
        this.attribute = attribute;
        this.duration = duration;
        this.modifier = modifier;
    }

    public void DecreaseDuration()
    {
        duration--;
    }
}
