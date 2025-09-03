using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class SpellDrawingUIController : MonoBehaviour
{
    public static SpellDrawingUIController Instance => instance;
    private static SpellDrawingUIController instance;

    [SerializeField] private GameObject window;

    [SerializeField] private GameObject spellChoiceButtonPrefab;
    [SerializeField] private GameObject buttonsParent;
    private ButtonReDrawSpells buttonReDrawSpells;

    private List<ButtonSpellChoice> buttons = new List<ButtonSpellChoice>();

    private void Awake()
    {
        if(!ReferenceEquals(SpellDrawingUIController.Instance, null))
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        buttonReDrawSpells = GetComponentInChildren<ButtonReDrawSpells>();
    }

    private void Start()
    {
        CreateChoiceButtons(SpellsController.Instance.SpellsAvaliableAtTheStartOfRound, SpellsController.Instance.SetCurrentSpellForThisTurnFromUnlockedSpells);
    }

    private void OnDisable()
    {
        instance = null;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    private void CreateChoiceButtons(int spellChoiceButtonsNumber, Action<int> chooseSpellForThisTurnMethod)
    {
        for (int i = 0; i < spellChoiceButtonsNumber; i++)
        {
            ButtonSpellChoice newButton = Instantiate(spellChoiceButtonPrefab, buttonsParent.transform).GetComponent<ButtonSpellChoice>();
            newButton.Initialize(i);
            buttons.Add(newButton);
        }
    }

    public void ShowWindowAndUpdateButtons(Action<int> MethodForButtons, Action MethodToReDrawSpells, List<BaseSpell> avaliableSpells)
    {
        window.SetActive(true);

        for(int i = 0; i < buttons.Count && i < avaliableSpells.Count; i++)
        {
            buttons[i].UpdateButton(SpellsController.Instance.SpellsAvaliableForThisTurn[i].SpellIcon, MethodForButtons, SpellsController.Instance.SpellsAvaliableForThisTurn[i].spellName);
        }

        buttonReDrawSpells.UpdateButtonMethod(MethodToReDrawSpells);
    }

    public void Refresh()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].UpdateButton(SpellsController.Instance.SpellsAvaliableForThisTurn[i].SpellIcon);
        }
    }

    public void HideWindow()
    {
        window.SetActive(false);
    }
}
