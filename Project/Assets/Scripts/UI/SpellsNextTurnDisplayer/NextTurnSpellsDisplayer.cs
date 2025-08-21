using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextTurnSpellsDisplayer : MonoBehaviour
{
    public static NextTurnSpellsDisplayer Instance => instance;
    private static NextTurnSpellsDisplayer instance;

    [SerializeField] private GameObject window;
    [SerializeField] private GameObject spellIconPrefab;

    private List<SpellNextTurnMiniature> miniatures = new List<SpellNextTurnMiniature>();

    private void Awake()
    {
        if(!ReferenceEquals(NextTurnSpellsDisplayer.Instance, null))
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        Create(SpellsController.Instance.SpellsAvaliableAtTheStartOfRound);
    }

    private void OnDestroy()
    {
        instance = null;
    }

    private void Create(int spellNumber)
    {
        for(int i = 0; i < spellNumber; i++)
        {
            miniatures.Add(Instantiate(spellIconPrefab, window.transform).GetComponent<SpellNextTurnMiniature>());
        }
    }

    private void UpdateMiniatures(List<BaseSpell> spellsNextTurn, List<BaseSpell> avaliableSpells)
    {
        for (int i = 0; i < miniatures.Count && i < avaliableSpells.Count; i++)
        {
            miniatures[i].UpdateMiniature(spellsNextTurn[i].SpellIcon);
        }
    }

    public void ShowAndUpdate(List<BaseSpell> spellsNextTurn)
    {
        window.SetActive(true);

        UpdateMiniatures(spellsNextTurn, SpellsController.Instance.AllAvaliableSpells);
    }

    public void Hide()
    {
        window.SetActive(false);
    }
}
