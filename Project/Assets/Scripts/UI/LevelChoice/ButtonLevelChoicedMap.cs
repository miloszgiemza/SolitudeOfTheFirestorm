using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace LevelChoiceScene
{
    public class ButtonLevelChoicedMap : BaseDynamicallyGeneratedButton
    {
        public RectTransform ButtonRectTransform => rectTransform;

        private TextMeshProUGUI buttonNumberText;
        private RectTransform rectTransform;

        protected override void Awake()
        {
            base.Awake();
            buttonNumberText = GetComponentInChildren<TextMeshProUGUI>();
            rectTransform = GetComponent<RectTransform>();
        }

        public override void Initialize(int buttonNumber, Sprite buttonIcon, Action<int> buttonMethod)
        {
            base.Initialize(buttonNumber, buttonIcon, buttonMethod);
            buttonNumberText.text = (buttonNumber + 1).ToString();
        }

        public void Initialize(int buttonNumber, Action<int> buttonMethod)
        {
            this.buttonNumber = buttonNumber;
            this.buttonMethod = buttonMethod;
            buttonNumberText.text = (buttonNumber + 1).ToString();
        }


        public void Initialize(int buttonNumber)
        {
            this.buttonNumber = buttonNumber;
            buttonNumberText.text = (buttonNumber + 1).ToString();
        }

        protected override IEnumerator WaitWithOnClickActionTillButtonReleased()
        {
            //yield return new WaitUntil(() => InputControllerGameplay.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainAction.WasReleasedThisFrame());
            yield return new WaitForSeconds(0f);
            GameController.Instance.LoadCurrenLevelPreset(LevelsPresetsController.Instance.ReturnLevelPreset(buttonNumber));
            SelectedLevelWindowController.Instance.ShowSelectedLevelWindow(GameController.Instance.CurrenLevelPresetToLoad.AvaliableEnemiesTypes, GameController.Instance.CurrenLevelPresetToLoad.CountOfEnemiesOfTier);
            //buttonMethod.Invoke(buttonNumber);
        }

        public void SetRectTransform(Vector2 newRect)
        {
            rectTransform.localPosition = newRect;
        }
    }
}