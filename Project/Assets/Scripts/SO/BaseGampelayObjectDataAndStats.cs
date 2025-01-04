using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGampelayObjectDataAndStats : ScriptableObject
{
    public Sprite Image => image;
    public bool Walkable => walkable;
    public int Damage => damage;


    [SerializeField] protected Sprite image;
    [SerializeField] protected bool walkable = false;
    [SerializeField] protected int damage = 1;
}
