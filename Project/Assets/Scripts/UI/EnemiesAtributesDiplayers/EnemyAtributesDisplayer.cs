using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class EnemyAtributesDisplayer : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI atributeHealth;
    [SerializeField] protected TextMeshProUGUI atributeSpeed;
    [SerializeField] protected TextMeshProUGUI atributeDamage;
    [SerializeField] protected TextMeshProUGUI atributeDefence;

    public void UpdateUI(int health, int speed, int damage, int defence)
    {
        atributeHealth.text = health.ToString();
        atributeSpeed.text = speed.ToString();
        atributeDamage.text = damage.ToString();
        atributeDefence.text = defence.ToString();
    }
}
