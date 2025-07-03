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
        if(GameController.Instance.CurrenLevelPresetToLoad.LevelNumber > PlayerPersistentDataLoadedAndUnpackedController.Instance.PlayerPersistentData.UnlockedLevels) PlayerPersistentDataLoadedAndUnpackedController.Instance.UpdateUnlockedLevels(GameController.Instance.CurrenLevelPresetToLoad.LevelNumber);

        spellsFromWhichToChooseSpellUnlock = LevelRewardsController.Instance.DrawSpellsAvaliableToChooseForUnlock(2, RewardsAvaliableForFinishingLevels.Instance.SpellsTier1, 
            RewardsAvaliableForFinishingLevels.Instance.SpellsTier2, RewardsAvaliableForFinishingLevels.Instance.SpellsTier3, GameController.Instance.CurrenLevelPresetToLoad.Tier1SpellChance,
            GameController.Instance.CurrenLevelPresetToLoad.Tier2SpellChance, GameController.Instance.CurrenLevelPresetToLoad.Tier3SpellChance);

        List<Sprite> buttonsIcons = new List<Sprite>();

        for(int i=0; i < spellsFromWhichToChooseSpellUnlock.Count; i++)
        {
            buttonsIcons.Add(spellsFromWhichToChooseSpellUnlock[i].SpellIcon);
        }

        VictoryUIController.Instance.ShowWindow(spellsFromWhichToChooseSpellUnlock.Count, buttonsIcons, UnlockSpellMethod);
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
