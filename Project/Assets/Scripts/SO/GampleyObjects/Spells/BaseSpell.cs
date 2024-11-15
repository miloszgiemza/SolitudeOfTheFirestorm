using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseSpell : ScriptableObject
{
    public string SpellName => spellName;
    public Sprite SpellIcon => spellIcon;
    public VFXID SpellVFX => spellVFX;
    public int Damage => damage;

    public List<Status> Statuses => statuses;

    [SerializeField] public string spellName;
    [SerializeField] protected Sprite spellIcon;
    [SerializeField] protected VFXID spellVFX;
    [SerializeField] protected int damage;

    [SerializeField] protected List<Status> statuses = new List<Status>();

    public abstract List<Tile> ReturnAffectedTiles(Tile[,] mapData, Vector2 cursorPos, int modifierRange);
    public abstract List<MapPosition> ReturnAffectedTilesPositions(Tile[,] mapData, Vector2 cursorPos, int modifierRange);
    public abstract List<MapPosition> ReturnTilesInRange(Tile[,] mapData, int modifierRange);
    public abstract bool TryCast(Tile[,] mapData, Vector2 cursorPos, int modifierDamage, int modifierRange, int modifierEffectLength);
}
