using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public abstract class BaseDropdown : MonoBehaviour
{
    protected abstract List<string> DropdownChoiceOptions { get; }

    protected TMP_Dropdown dropdown;

    protected virtual void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
    }

    protected virtual void OnEnable()
    {
        InitializeDropdownChoiceOptions(DropdownChoiceOptions);

        dropdown.onValueChanged.AddListener(delegate { HandleDropdownValueChange(dropdown); });                                   
    }

    protected abstract void HandleDropdownValueChange(TMP_Dropdown dropdown);

    protected virtual void InitializeDropdownChoiceOptions(List<string> dropdownChoiceOptions)
    {
        dropdown.AddOptions(dropdownChoiceOptions);
    }
}

