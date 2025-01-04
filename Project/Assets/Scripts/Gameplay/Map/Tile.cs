using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct MapPosition
{
    public int X => x;
    public int Y => y;

    [SerializeField] private int x;
    [SerializeField] private int y;

    public MapPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void SetValues(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public class Tile
{
    public MapPosition Position => position;

    private MapPosition position;

    public EnemyNormal EnemySocket => enemySocket;
    public EnemyFlying EnemyFlying => enemyFLyingSocket;
    public Obstacle ObstacleSocket => obstacleSocket;
    public CollectableItem CollectableItemSocket => collectableItemSocket;

    private EnemyNormal enemySocket;
    private EnemyFlying enemyFLyingSocket;
    private Obstacle obstacleSocket;
    private CollectableItem collectableItemSocket;

    public Tile(MapPosition position)
    {
        this.position = position;

        enemySocket = null;
        enemyFLyingSocket = null;
        obstacleSocket = null;
    }

    public void UpdateTile(EnemyNormal enemy)
    {
        enemySocket = enemy;
    }

    public void UpdateTile(EnemyFlying enemyFlying)
    {
        enemyFLyingSocket = enemyFlying;
    }

    public void UpdateTile(Obstacle obstacle)
    {
        obstacleSocket = obstacle;
    }

    public void UpdateTile(CollectableItem collectableItem)
    {
        collectableItemSocket = collectableItem;
    }

    public void Clear(EnemyNormal enemy)
    {
        enemySocket = null;
    }

    public void Clear(EnemyFlying enemyFlying)
    {
        enemyFLyingSocket = null;
    }

    public void Clear(Obstacle obstacle)
    {
        obstacleSocket = null;
    }

    public void Clear(CollectableItem collectableItem)
    {
        collectableItemSocket = null;
    }

    public void Clear(GameplayObjectType socketType)
    {
        switch(socketType)
        {
            case GameplayObjectType.Enemy:
                enemySocket = null;
                break;
            case GameplayObjectType.EnemyFlying:
                enemyFLyingSocket = null;
                break;
            case GameplayObjectType.Obstacle:
                obstacleSocket = null;
                break;
            case GameplayObjectType.CollectableItem:
                collectableItemSocket = null;
                break;
        }
    }

    public bool CheckIfPossibleToMoveOn(EnemyNormal enemy)
    {
        bool possible = true;

        if(!ReferenceEquals(enemySocket, null))
        {
            possible = false;
        }
        else if(!ReferenceEquals(obstacleSocket, null))
        {
            if(!obstacleSocket.Walkable)
            {
                possible = false;
            }
        }

        return possible;
    }

    public bool CheckIfPossibleToMoveOn(EnemyFlying enemyFlying)
    {
        bool possible = true;

        if(!ReferenceEquals(enemyFLyingSocket, null))
        {
            possible = false;
        }

        return possible;
    }

    public bool CheckIfPossibleToMoveOn(Obstacle obstacle)
    {
        return CheckIfPossibleToCreateObbstacle();
    }

    public bool CheckIfPossibleToMoveOn(CollectableItem collectableItem)
    {
        return true;
    }

    public bool CheckIfPossibleToCreateObbstacle()
    {
        bool possible = true;

        if(!ReferenceEquals(enemySocket, null))
        {
            possible = false;
        }

        return possible;
    }

    public void MakeObjectsOnTileReact(BaseSpell spell, int spellDamage, int modifierDamage, int modifierRange, int modifierEffectLength)
    {
        if (!ReferenceEquals(collectableItemSocket, null))
        {
            collectableItemSocket.ReactToSpell(spell, spellDamage, modifierDamage, modifierEffectLength);
        }
        if (!ReferenceEquals(enemySocket, null))
        {
            enemySocket.ReactToSpell(spell, spellDamage, modifierDamage, modifierEffectLength);
        }
        if(!ReferenceEquals(enemyFLyingSocket, null))
        {
            enemyFLyingSocket.ReactToSpell(spell, spellDamage, modifierDamage, modifierEffectLength);
        }

        EnemiesController.Instance.ClearDeadEnemies();
    }

    public void MakeObjectsOnTileReact(List<Status> statuses)
    {
        if (!ReferenceEquals(enemySocket, null))
        {
            enemySocket.ReactToAura(statuses);
        }
        if (!ReferenceEquals(enemyFLyingSocket, null))
        {
            enemyFLyingSocket.ReactToAura(statuses);
        }

        EnemiesController.Instance.ClearDeadEnemies();
    }
}
