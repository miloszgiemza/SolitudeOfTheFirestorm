using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class Player : MonoBehaviour
{
    public static Player Instance => instance;

    private static Player instance;

    public GameplayController GameplayController => gameplayController;

    public Action OnSpellSpent;
    public BasePlayerState CurrentState => currentState;

    public int SecendaryActionsAvaliable => secendaryActionsAvaliable;

    private GameplayController gameplayController;

    private BasePlayerState currentState;

    private StateIdle stateIdle = new StateIdle();
    private StateChooseSpellForThisTurn stateChooseSpellForThisTurn = new StateChooseSpellForThisTurn();
    private StateCastingSpell stateCastingSpell = new StateCastingSpell();
    private StateDeactivated stateDeactivated = new StateDeactivated();
    private StateCastingSpellFromBigScroll stateCastingSpellFromBig = new StateCastingSpellFromBigScroll();
    private StateCastingSpellFromSmallScroll stateCastingSpellFromSmallScroll = new StateCastingSpellFromSmallScroll();
    private StateDrinkMixture stateDrinkMixture = new StateDrinkMixture();

    private StateIdleDebugMode stateIdleDebugMode = new StateIdleDebugMode();
    private StateCastingSpellInDebugMode stateCastingSpellInDebugMode = new StateCastingSpellInDebugMode();
    private StateCastingSpellFromSmallScrollInDebugMode stateCastingSpellFromSmallScrollInDebugMode = new StateCastingSpellFromSmallScrollInDebugMode();
    private StateDrinkingMixtureInDebugMode stateDrinkingMixtureInDebugMode = new StateDrinkingMixtureInDebugMode();
    private StateCastingSpellFromBigScrollInDebugMode stateCastingSpellFromBigScrollInDebugMode = new StateCastingSpellFromBigScrollInDebugMode();

    public int Health => health;

    private int mainActionsLimit = 1;
    private int mainActionsRemaining;

    [SerializeField] private int health = 1;

    private int secendaryActionsLimit = 1;
    private int secendaryActionsAvaliable = 1;

    private void Awake()
    {
        if(Player.Instance!=null)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }

        currentState = stateDeactivated;
        gameplayController = GetComponentInParent<GameplayController>();
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void StartPlayerTurn()
    {
        ApplyStatuses();
        SwitchState(PlayerState.ChooseSpellForThisTurn);
        
        mainActionsRemaining = 1;
        secendaryActionsAvaliable = secendaryActionsLimit;

        ActionsCounter.Instance.Refresh(mainActionsRemaining.ToString(), secendaryActionsAvaliable.ToString());
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

                if(mainActionsRemaining > 0)
                {
                    currentState.EndState(this);
                    currentState = stateCastingSpell;
                    currentState.StartState(this);
                }
     
                break;

            case PlayerState.CastingSpellFromSmallScroll:
                if (secendaryActionsAvaliable > 0)
                {
                    currentState.EndState(this);
                    currentState = stateCastingSpellFromSmallScroll;
                    currentState.StartState(this);
                }
                break;

            case PlayerState.DrinkingMixtureInDebugMode:
                currentState.EndState(this);
                currentState = stateDrinkingMixtureInDebugMode;
                currentState.StartState(this);
                break;

            case PlayerState.CastingSpellFromBigScroll:
                if (mainActionsRemaining > 0)
                {
                    currentState.EndState(this);
                    currentState = stateCastingSpellFromBig;
                    currentState.StartState(this);
                }
                break;

            case PlayerState.DrinkingMixture:
                if (secendaryActionsAvaliable > 0)
                {
                    currentState.EndState(this);
                    currentState = stateDrinkMixture;
                    currentState.StartState(this);
                }
                break;

            case PlayerState.IdleDebugMode:
                currentState.EndState(this);
                currentState = stateIdleDebugMode;
                currentState.StartState(this);
                    break;

            case PlayerState.CastingSpellInDebugMode:
                currentState.EndState(this);
                currentState = stateCastingSpellInDebugMode;
                currentState.StartState(this);
                break;

            case PlayerState.CastingSpellFromSmallScrollInDebugMode:
                currentState.EndState(this);
                currentState = stateCastingSpellFromSmallScrollInDebugMode;
                currentState.StartState(this);
                break;

            case PlayerState.CastingSpellFromBigScrollDebugMode:
                currentState.EndState(this);
                currentState = stateCastingSpellFromBigScrollInDebugMode;
                currentState.StartState(this);
                break;
        }
    }

    public void SetSpellToSpentThisTurn()
    {
        mainActionsRemaining--;
        ActionsCounter.Instance.Refresh(mainActionsRemaining.ToString(), secendaryActionsAvaliable.ToString());
        if (!ReferenceEquals(OnSpellSpent, null)) OnSpellSpent.Invoke();
    }

    public void SpendSecendaryAction()
    {
        secendaryActionsAvaliable--;
        ActionsCounter.Instance.Refresh(mainActionsRemaining.ToString(), secendaryActionsAvaliable.ToString());
    }

    public void DeactivatePlayerAtEndOfTurn()
    {
        currentState = stateDeactivated;
        mainActionsRemaining = mainActionsLimit;

        SpellDrawingUIController.Instance.HideWindow();
        NextTurnSpellsDisplayer.Instance.Hide();
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

    public void AcquireStatuses(List<Status> newStatuses)
    {
        for (int currentNewStatus = 0; currentNewStatus < newStatuses.Count; currentNewStatus++)
        {
            attributes[newStatuses[currentNewStatus].Attribute].AddStatus(new Status(newStatuses[currentNewStatus].Attribute, newStatuses[currentNewStatus].Duration, newStatuses[currentNewStatus].Modifier));
        }

        ApplyStatuses();
    }

    protected void ApplyStatuses()
    {
        foreach (KeyValuePair<AttributeID, BaseAttribute> attribute in attributes)
        {
            attribute.Value.ApplyModifiersAtStartOfTurn();
        }
    }
    #endregion

    public void CancelCurrentAction()
    {
        SwitchState(PlayerState.Idle);
    }
}
