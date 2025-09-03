using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Pathfinding;

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

    public Node NodeEnemyWalking => nodeEnemyWalking;
    public Node NodeEnemyFlying => nodeEnemyFlying;

    private EnemyNormal enemySocket;
    private EnemyFlying enemyFLyingSocket;
    private Obstacle obstacleSocket;
    private CollectableItem collectableItemSocket;

    #region Pathfinding
    private Node nodeEnemyWalking = new Node();
    private Node nodeEnemyFlying = new Node();
    #endregion

    public Tile(MapPosition position)
    {
        this.position = position;

        enemySocket = null;
        enemyFLyingSocket = null;
        obstacleSocket = null;

        nodeEnemyWalking.Initialize(position.X, position.Y, true, 0f, 0f);
        nodeEnemyFlying.Initialize(position.X, position.Y, true, 0f, 0f);

        nodeEnemyWalking = new Node(position.X, position.Y, true, 0f, 0f);
        nodeEnemyFlying = new Node(position.X, position.Y, true, 0f, 0f);
    }

    public void UpdateTile(EnemyNormal enemy)
    {
        enemySocket = enemy;
        nodeEnemyWalking.SetTraversable(false);
    }

    public void UpdateTile(EnemyRanged enemy)
    {
        enemySocket = enemy;
        nodeEnemyWalking.SetTraversable(false);
    }

    public void UpdateTile(EnemyFlying enemyFlying)
    {
        enemyFLyingSocket = enemyFlying;
        nodeEnemyFlying.SetTraversable(false);
    }

    public void UpdateTile(Obstacle obstacle)
    {
        obstacleSocket = obstacle;
        if(!obstacle.Walkable) nodeEnemyWalking.SetTraversable(false); 
        else
        {
            nodeEnemyWalking.SetMovementCostReal(obstacleSocket.MovementCostReal);
            nodeEnemyWalking.SetMovementCostHeuristic(obstacleSocket.MovementCostHeuristic);
        }
    }

    public void UpdateTile(CollectableItem collectableItem)
    {
        collectableItemSocket = collectableItem;
    }

    public void Clear(EnemyNormal enemy)
    {
        enemySocket = null;
        nodeEnemyWalking.SetTraversable(true);
    }

    public void Clear(EnemyRanged enemy)
    {
        enemySocket = null;
        nodeEnemyWalking.SetTraversable(true);
    }

    public void Clear(EnemyFlying enemyFlying)
    {
        enemyFLyingSocket = null;
        nodeEnemyFlying.SetTraversable(true);
    }

    public void Clear(Obstacle obstacle)
    {
        obstacleSocket = null;
        
        nodeEnemyWalking.SetTraversable(true);
        nodeEnemyWalking.SetMovementCostReal(0f);
        nodeEnemyWalking.SetMovementCostHeuristic(0f);
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
                nodeEnemyWalking.SetTraversable(true);
                break;
            case GameplayObjectType.EnemyWithAreaAura:
                enemySocket = null;
                nodeEnemyWalking.SetTraversable(true);
                break;
            case GameplayObjectType.EnemyRanged:
                enemySocket = null;
                nodeEnemyWalking.SetTraversable(true);
                break;
            case GameplayObjectType.EnemyFlying:
                enemyFLyingSocket = null;
                nodeEnemyFlying.SetTraversable(true);
                break;
            case GameplayObjectType.Obstacle:
                obstacleSocket = null;
                
                nodeEnemyWalking.SetTraversable(true);
                nodeEnemyWalking.SetMovementCostReal(0f);
                nodeEnemyWalking.SetMovementCostHeuristic(0f);
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

    public bool CheckIfPossibleToMoveOn(EnemyRanged enemy)
    {
        bool possible = true;

        if (!ReferenceEquals(enemySocket, null))
        {
            possible = false;
        }
        else if (!ReferenceEquals(obstacleSocket, null))
        {
            if (!obstacleSocket.Walkable)
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

    public int ReturnObjectReceivedDamage(int spellDamage, int modifierDamage)
    {
        int damageReceived = 0;

        if(ReferenceEquals(enemySocket, null))
        {
            damageReceived = 0;
        }
        else
        {
            damageReceived = enemySocket.ReturnReceivedDamage(
                SpellsController.Instance.CurrentSpell.ReturnDamage(this, Map.Instance.MapData, InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>(),
                Player.Instance.Attributes[AttributeID.PlayerModifierSpellRange].CurrentValue), modifierDamage);
        }

        return damageReceived;
    }
}
