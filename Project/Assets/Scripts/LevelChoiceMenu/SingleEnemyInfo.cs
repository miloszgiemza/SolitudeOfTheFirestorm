using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

namespace LevelChoiceScene
{
    public class SingleEnemyInfo : MonoBehaviour
    {
        [SerializeField] private Image miniatureImage;
        [SerializeField] private TextMeshProUGUI textHealth;
        [SerializeField] private TextMeshProUGUI textDefence;
        [SerializeField] private TextMeshProUGUI textSpeed;
        [SerializeField] private TextMeshProUGUI textAttack;

        public void InitializeSingleEnemy(EnemyData enemyPreset)
        {
            miniatureImage.sprite = enemyPreset.Image;
            textHealth.text = "Health: " + enemyPreset.Health.ToString();
            textDefence.text = "Defence: " + enemyPreset.Defence.ToString();
            textSpeed.text = "Speed: " + enemyPreset.Speed.ToString();
            textAttack.text = "Damage: " + enemyPreset.Damage.ToString();
        }
    }
}
