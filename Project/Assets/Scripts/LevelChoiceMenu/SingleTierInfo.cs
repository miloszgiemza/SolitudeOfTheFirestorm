using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

namespace LevelChoiceScene
{
    public class SingleTierInfo : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        public void Initialize(string text)
        {
            this.text.text = text;
        }
    }
}