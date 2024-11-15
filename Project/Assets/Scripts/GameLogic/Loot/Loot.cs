using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[Serializable]
public class Loot
{
    public int ProbabilityOfDrop => probabilityOfDrop;
    public BaseItem Item => item;

    [Range(0, 100)]
    [SerializeField] protected int probabilityOfDrop;

    [SerializeField] protected BaseItem item;
}
