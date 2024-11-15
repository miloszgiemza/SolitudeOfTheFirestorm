using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class Player : MonoBehaviour
{
    public static Player Instance => instance;

    private static Player instance;

    public Action OnSpellSpent;
    public BasePlayerState CurrentState => currentState;

    private BasePlayerState currentState;

    private StateIdle stateIdle = new StateIdle();
    private StateChooseSpellForThisTurn stateChooseSpellForThisTurn = new StateChooseSpellForThisTurn();
    private StateCastingSpell stateCastingSpell = new StateCastingSpell();
    private StateUsingItem stateUsingItem = new StateUsingItem();
    private StateDeactivated stateDeactivated = new StateDeactivated();
    private StateCastingSpellFromBigScroll stateCastingSpellFromBig = new StateCastingSpellFromBigScroll();
    private StateCastingSpellFromSmallScroll stateCastingSpellFromSmallScroll = new StateCastingSpellFromSmallScroll();

    public int Health => health;

    private GameplayController gameplayController;

    private bool spellSpent;

    private int health = 10;

    private void Awake()
    {
        if(Player.Instance!=null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        currentState = stateDeactivated;

        gameplayController = GetComponentInParent<GameplayController>();
    }

    public void StartPlayerTurn()
    {
        ApplyStatuses();
        SwitchState(PlayerState.ChooseSpellForThisTurn);
        spellSpent = false;
    }

    public void RunUpdate(GameplayController game)
    {
        currentState.RunUpdate(this, game);
    }

    public void SwitchState(PlayerState newState)
    {
        switch(newState)
        {
            case PlayerState.Idle:
                currentState.EndState(this);
                currentState = stateIdle;
                currentState.StartState(this);
                break;

            case PlayerState.ChooseSpellForThisTurn:
                currentState.EndState(this);
                currentState = stateChooseSpellForThisTurn;
                currentState.StartState(this);
                break;

            case PlayerState.Casting:

                if(!spellSpent)
                {
                    currentState.EndState(this);
                    currentState = stateCastingSpell;
                    currentState.StartState(this);
                }
     
                break;

            case PlayerState.CastingSpellFromBigScroll:
                if (!spellSpent)
                {
                    currentState.EndState(this);
                    currentState = stateCastingSpellFromBig;
                    currentState.StartState(this);
                }
                break;

            case PlayerState.CastingSpellFromSmallScroll:
                currentState.EndState(this);
                currentState = stateCastingSpellFromSmallScroll;
                currentState.StartState(this);
                break;

            case PlayerState.UsingItem:
                currentState.EndState(this);
                currentState = stateUsingItem;
                currentState.StartState(this);
                break;
        }
    }

    public void SetSpellToSpentThisTurn()
    {
        spellSpent = true;
        if (!ReferenceEquals(OnSpellSpent, null)) OnSpellSpent.Invoke();
    }

    public void DeactivatePlayerAtEndOfTurn()
    {
        currentState = stateDeactivated;
        spellSpent = true;
    }

    public void ReceiveDamage(int damage)
    {
        health -= damage;
    }

    #region Attributes
    public Dictionary<AttributeID, BaseAttribute> Attributes => attributes;
    protected Dictionary<AttributeID, BaseAttribute> attributes = new Dictionary<AttributeID, BaseAttribute>
    {
        { AttributeID.PlayerModifierSpellDamage,  new AttributePlayerSpellDamageModifier(0, 0) },
        { AttributeID.PlayerModifierSpellRange, new AttributePlayerSpellRangeModifier(0, 0) },
        {AttributeID.PlayerModifierSpellEffectDuration, new AttributePlayerSpellEffectDurationModifier(0, 0) }
    };

    protected void AcquireStatuses(List<Status> newStatuses)
    {
        for (int currentNewStatus = 0; currentNewStatus < newStatuses.Count; currentNewStatus++)
        {
            attributes[newStatuses[currentNewStatus].Attribute].AddStatus(new Status(newStatuses[currentNewStatus].Attribute, newStatuses[currentNewStatus].Duration, newStatuses[currentNewStatus].Modifier));
        }
    }

    protected void ApplyStatuses()
    {
        foreach (KeyValuePair<AttributeID, BaseAttribute> attribute in attributes)
        {
            attribute.Value.ApplyModifiersAtStartOfTurn();
        }
    }
    #endregion
}
