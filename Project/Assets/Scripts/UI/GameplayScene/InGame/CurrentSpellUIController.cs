using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentSpellUIController : MonoBehaviour
{
    private TextMeshProUGUI textSpellName;
    private Image imageSpellIcon;

    private void Awake()
    {
        textSpellName = GetComponentInChildren<TextMeshProUGUI>();
        imageSpellIcon = GetComponentInChildren<Image>();
    }

    private void OnEnable()
    {
        StartCoroutine(WaitForSingletonsToInitialize());
    }

    private void OnDisable()
    {
        SpellsController.Instance.OnUpdateCurrentSpell -= UpdateCurrentSpellUI;
    }

    private IEnumerator WaitForSingletonsToInitialize()
    {
        yield return new WaitUntil(() => !ReferenceEquals(SpellsController.Instance, null));
        yield return new WaitUntil(() => !ReferenceEquals(Player.Instance, null));
        SpellsController.Instance.OnUpdateCurrentSpell += UpdateCurrentSpellUI;
        Player.Instance.OnSpellSpent += GreyOutIcon;
    }

    private void UpdateCurrentSpellUI(string name, Sprite icon)
    {
        imageSpellIcon.color = new Color(255, 255, 255, 255);
        textSpellName.text = name;
        imageSpellIcon.sprite = icon;
    }

    private void GreyOutIcon()
    {
        imageSpellIcon.color = new Color32(34, 32, 32, 255);
    }
}
