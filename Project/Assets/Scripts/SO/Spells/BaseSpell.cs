using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseSpell : BaseGameDatabaseItem
{
    public override DatabaseItemType DatabaseItemType => DatabaseItemType.Spell;

    public string SpellName => spellName;
    public Sprite SpellIcon => spellIcon;
    public VFXID SpellVFX => spellVFX;

    public List<Status> Statuses => statuses;

    [SerializeField] public string spellName;
    [SerializeField] protected Sprite spellIcon;
    [SerializeField] protected VFXID spellVFX;
    [SerializeField] protected int damage;

    [SerializeField] protected List<Status> statuses = new List<Status>();

    public abstract List<Tile> ReturnAffectedTiles(Tile[,] mapData, Vector2 cursorPos, int modifierRange); //uwzglêdnia modyfiaktor gracza
    public abstract List<MapPosition> ReturnAffectedTilesPositions(Tile[,] mapData, Vector2 cursorPos, int modifierRange); //uwzglêdnia modyfikator gracza
    public abstract List<MapPosition> ReturnTilesInRange(Tile[,] mapData, int modifierRange); //uwzglêdnia modyfikator gracza
    public abstract bool TryCast(Tile[,] mapData, Vector2 cursorPos, int modifierDamage, int modifierRange, int modifierEffectLength); //uwzglêdnia modyfikator gracza
    public abstract int ReturnDamage();
    public abstract int ReturnDamage(Tile tile, Tile[,] mapData, Vector2 cursorPos, int modifierRange);
}
