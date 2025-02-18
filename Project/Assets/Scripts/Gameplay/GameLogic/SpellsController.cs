using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class SpellsController : MonoBehaviour
{
    public Action<string, Sprite> OnUpdateCurrentSpell;

    public static SpellsController Instance => instance;
    private static SpellsController instance;

    public BaseSpell CurrentSpell => currentSpell;

    public List<BaseSpell> AvaliableSpells => avaliableSpells;

    public int SpellsAvaliableAtTheStartOfRound => spellsAvaliableAtTheStartOfRound;
    public List<BaseSpell> SpellsAvaliableForThisTurn => spellsAvaliableForThisTurn;
    public List<BaseSpell> SpellsAvaliableForNextTurn => spellsAvaliableForNextTurn;

    public int SpellReDraws => spellReDrawsPerLevel;

    public bool InvertedAreaOfEffect => invertedAreaOfEffect;

    private BaseSpell currentSpell;

    private List<BaseSpell> avaliableSpells;
    [SerializeField] private List<BaseSpell> defaultStartingSpells;

    private int spellsAvaliableAtTheStartOfRound = 2;
    private List<BaseSpell> spellsAvaliableForThisTurn = new List<BaseSpell>();
    private List<BaseSpell> spellsAvaliableForNextTurn = new List<BaseSpell>();

    private int spellReDrawsPerLevel = 3;

    private BaseSpell defaultCurrentSpell = null;

    private bool invertedAreaOfEffect = false;

    private void Awake()
    {
        if(!ReferenceEquals(SpellsController.Instance, null))
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        LoadUnlockedSpells(PlayerProgressionController.Instance.PlayerProgression.UnlockedSpells);
        
        currentSpell = ListRandomElement<BaseSpell>.ReturnRandomElement(avaliableSpells);
    }

    private void OnDestroy()
    {
        instance = null;
    }

    #region Initialisation
    private void LoadUnlockedSpells(List<string> unlockedSpellsIDs)
    {
        if(SaveLoadController.CheckIfSaveFileExists())
        {
            avaliableSpells = new List<BaseSpell>();

            for (int i = 0; i < unlockedSpellsIDs.Count; i++)
            {
                BaseSpell loadedSpell = Database.Instance.DatabaseSpells.ReturnItem(unlockedSpellsIDs[i]);

                if (!ReferenceEquals(loadedSpell, null))
                {
                    avaliableSpells.Add(loadedSpell);
                }
            }
        }
        else
        {
            avaliableSpells = defaultStartingSpells;
        }
    }
    #endregion

    public void SetCurrentSpellForThisTurn(int chosenSpell)
    {
        currentSpell = spellsAvaliableForThisTurn[chosenSpell];

        if(!ReferenceEquals(OnUpdateCurrentSpell, null))OnUpdateCurrentSpell.Invoke(currentSpell.SpellName, currentSpell.SpellIcon);
    }

    private List<BaseSpell> DrawNonDuplicateSpells(int spellsNumber, List<BaseSpell> avaliableSpells)
    {
        List<BaseSpell> drawnSpells = new List<BaseSpell>();
        List<BaseSpell> currentPoolToDraw = new List<BaseSpell>();

        foreach(BaseSpell avaliableSpell in avaliableSpells)
        {
            currentPoolToDraw.Add(avaliableSpell);
        }

        for(int i = 0; i < spellsNumber && currentPoolToDraw.Count > 0; i++)
        {
            int drawnSpell = UnityEngine.Random.Range(0, currentPoolToDraw.Count);
            drawnSpells.Add(currentPoolToDraw[drawnSpell]);
            currentPoolToDraw.RemoveAt(drawnSpell);
        }

        return drawnSpells;
    }

    public void DrawSpellsForThisAndNextRound()
    {
        if(spellsAvaliableForNextTurn.Count <= 0)
        {
            spellsAvaliableForThisTurn = DrawNonDuplicateSpells(spellsAvaliableAtTheStartOfRound, avaliableSpells);
            spellsAvaliableForNextTurn = DrawNonDuplicateSpells(spellsAvaliableAtTheStartOfRound, avaliableSpells);
        }
        else
        {
            spellsAvaliableForThisTurn = spellsAvaliableForNextTurn;
            spellsAvaliableForNextTurn = DrawNonDuplicateSpells(spellsAvaliableAtTheStartOfRound, avaliableSpells);
        }
    }

    public void ReDrawSpells()
    {
        if(spellReDrawsPerLevel > 0)
        {
            spellReDrawsPerLevel--;

            spellsAvaliableForThisTurn = DrawNonDuplicateSpells(spellsAvaliableAtTheStartOfRound, avaliableSpells);
        }
    }

    public void EquipTemporarySpell(BaseSpell temporarySpell)
    {
        defaultCurrentSpell = currentSpell;
        currentSpell = temporarySpell;
    }

    public void UnEquipTemporarySpell()
    {
        currentSpell = defaultCurrentSpell;
        defaultCurrentSpell = null;
    }

    public void InvertAreaOfEffect()
    {
        if (invertedAreaOfEffect) invertedAreaOfEffect = false;
        else invertedAreaOfEffect = true;
    }
}
