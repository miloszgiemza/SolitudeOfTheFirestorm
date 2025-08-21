using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum ItemType
{
    BigScroll,
    Mixture,
    SmallScrolls
}

public abstract class BaseItem : BaseGameDatabaseItem
{
    public override DatabaseItemType DatabaseItemType => DatabaseItemType.Item;

    public abstract ItemType ItemType { get; }

    public string Name => name;
    public Sprite Icon => icon;
    public Sprite IconCollectable => iconCollectable;

    [SerializeField] protected string name = "";
    [SerializeField] protected Sprite icon;
    [SerializeField] protected Sprite iconCollectable;

    public abstract void TryUseItem();
    public abstract void ClearAfterItemUse();
}
