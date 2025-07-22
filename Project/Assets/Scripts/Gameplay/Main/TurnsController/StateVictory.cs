using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LevelChoiceScene;

public class StateVictory : BaseGameState
{
    public override GameplayController.States StateID => GameplayController.States.Victory;

    List<BaseSpell> spellsFromWhichToChooseSpellUnlock;

    public override void StartTurn(GameplayController game)
    {
        if(GameController.Instance.CurrenLevelPresetToLoad.LevelNumber + 1 > PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.UnlockedLevels)
        {
            spellsFromWhichToChooseSpellUnlock = LevelRewardsController.Instance.DrawSpellsAvaliableToChooseForUnlock(2, GameController.Instance.CurrenLevelPresetToLoad.SpellsUnlocksAvaliableAsRewards);
            
            PlayerPersistentDataLoadedAndUnpackedController.Instance.UpdateUnlockedLevels(GameController.Instance.CurrenLevelPresetToLoad.LevelNumber + 1);
            
            if (!ReferenceEquals(spellsFromWhichToChooseSpellUnlock, null) && spellsFromWhichToChooseSpellUnlock.Count > 1)
            {
                List<Sprite> buttonsIcons = new List<Sprite>();

                for (int i = 0; i < spellsFromWhichToChooseSpellUnlock.Count; i++)
                {
                    buttonsIcons.Add(spellsFromWhichToChooseSpellUnlock[i].SpellIcon);
                }

                VictoryUIController.Instance.ShowWindow(spellsFromWhichToChooseSpellUnlock.Count, buttonsIcons, UnlockSpellMethod);
            }
            else
            {
                GameController.Instance.LoadMainMenuScene();
            }
        }
        else
        {
            GameController.Instance.LoadMainMenuScene();
        }
  
    }

    public override void RunUpdate(GameplayController game)
    {
    }

    public override void EndTurn(GameplayController game)
    {
    }

    protected void UnlockSpellMethod(int spellToUnlock)
    {
        PlayerPersistentDataLoadedAndUnpackedController.Instance.UnlockSpell(spellsFromWhichToChooseSpellUnlock[spellToUnlock]);
        GameController.Instance.LoadMainMenuScene();
    }
}
