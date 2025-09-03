using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LevelChoiceScene
{
    public class LevelChoiceMenuUIController : MonoBehaviour
    {
        [SerializeField] private GameObject buttonPrefab;

        private List<ButtonLevelChoicedMap> buttons = new List<ButtonLevelChoicedMap>();

        private Vector2 firstButtonPos = new Vector2(59.9658f, 842f);
        private float spaceBeetwenButtons = 110f;

        private LevelChoiceSceneController levelChoiceSceneController;

        private void Awake()
        {
            levelChoiceSceneController = GetComponentInParent<LevelChoiceSceneController>();
        }

        private void Start()
        {
            CreateButtons(PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.UnlockedLevels);
        }

        private void CreateButtons(int unlockedLevelsNumber)
        {
            Vector2 buttonRectTransform = firstButtonPos;

            for(int i=0; i <= unlockedLevelsNumber; i++)
            {
                buttons.Add(Instantiate(buttonPrefab, this.transform).GetComponent<ButtonLevelChoicedMap>());
                buttons[buttons.Count-1].Initialize(i);
                buttons[buttons.Count-1].SetRectTransform(buttonRectTransform);

                buttonRectTransform = new Vector2(buttonRectTransform.x, buttonRectTransform.y - spaceBeetwenButtons);
            }
        }
    }
}

