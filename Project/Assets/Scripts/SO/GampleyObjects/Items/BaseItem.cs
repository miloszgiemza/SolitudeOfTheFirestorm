using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    BigScroll,
    Mixture,
    SmallScrolls
}

public abstract class BaseItem : ScriptableObject
{
    public abstract ItemType ItemType { get; }

    public Sprite Icon => icon;
    public Sprite IconCollectable => iconCollectable;

    [SerializeField] protected Sprite icon;
    [SerializeField] protected Sprite iconCollectable;

    public abstract void TryUseItem();
    public abstract void ClearAfterItemUse();
}
