using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro; 

public class TileUIDisplayer : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI textPotentialDamageValue;

    public void UpdatePotentialDamageValue(string potentialDamageValue)
    {
        textPotentialDamageValue.gameObject.SetActive(true);
        textPotentialDamageValue.text = potentialDamageValue;
    }

    public void HidePotentialDamageValue()
    {
        textPotentialDamageValue.text = "";
        textPotentialDamageValue.gameObject.SetActive(false);
    }
}
