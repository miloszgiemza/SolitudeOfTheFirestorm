using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCastingSpellFromSmallScroll : BasePlayerState
{
    public override PlayerState State => PlayerState.CastingSpellFromSmallScroll;

    public override void StartState(Player player)
    {
    }

    public override void EndState(Player player)
    {
    }

    public override void RunUpdate(Player player, GameplayController gameplayController)
    {
        if (InputControllerGameplay.Instance.MainInputAssetsWrapper.MobileDevicesMap.EndTurnOrCancelAction.WasReleasedThisFrame())
        {
            PlayerInventoryController.Instance.DeselectItemAndNullCurrentItemOnUnsuccesfullUse();
            EndCasting();
        }
        else if (InputControllerGameplay.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainAction.WasReleasedThisFrame())
        {
            TryCastSpell(InputControllerGameplay.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>(), 
                Player.Instance.Attributes[AttributeID.PlayerModifierSpellDamage].CurrentValue, Player.Instance.Attributes[AttributeID.PlayerModifierSpellRange].CurrentValue, 
                Player.Instance.Attributes[AttributeID.PlayerModifierSpellEffectDuration].CurrentValue);
        }

        TilesHighlighterTilesInRange.Instance.UpdateHighlightedTiles(SpellsController.Instance.CurrentSpell.ReturnTilesInRange(Map.Instance.MapData, 
            Player.Instance.Attributes[AttributeID.PlayerModifierSpellRange].CurrentValue));
        TilesHighlighterSelectedTiles.Instance.UpdateHighlightedTiles(SpellsController.Instance.CurrentSpell.ReturnAffectedTilesPositions(Map.Instance.MapData,
            InputControllerGameplay.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>(), Player.Instance.Attributes[AttributeID.PlayerModifierSpellRange].CurrentValue));
    }

    private void EndCasting()
    {
        //wy��cz pod�wietlenie
        Player.Instance.SwitchState(PlayerState.Idle);
    }

    private void TryCastSpell(Vector2 cursorPos, int modifierDamage, int modifierRange, int modifierEffectLength)
    {
        if (SpellsController.Instance.CurrentSpell.TryCast(Map.Instance.MapData, InputControllerGameplay.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>(),
            modifierDamage, modifierRange, modifierEffectLength))
        {
            PlayerInventoryController.Instance.DestroyItemAndNullCurrentItemOnSuccesfullUse();

            WrapUpAfterSuccesfullSpell(Map.Instance.MapData, InputControllerGameplay.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>(),
                SpellsController.Instance.CurrentSpell, modifierRange);
        }
        else
        {
            PlayerInventoryController.Instance.DeselectItemAndNullCurrentItemOnUnsuccesfullUse();

            EndCasting();
        }
    }

    private void WrapUpAfterSuccesfullSpell(Tile[,] mapData, Vector2 cursorPos, BaseSpell spell, int modifierRange)
    {
        VFXController.Instance.Play(spell.SpellVFX, SpellsController.Instance.CurrentSpell.ReturnAffectedTilesPositions(mapData,
            cursorPos, modifierRange));
        EnemiesDisplayControler.Instance.Display(EnemiesController.Instance.EnemiesOnMap);
        EnemiesNormalAtributesUIDisplayController.Instance.UpdateUI(Map.Instance.MapData);

        EndCasting();
    }
}