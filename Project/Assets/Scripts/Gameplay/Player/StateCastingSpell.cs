using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCastingSpell : BasePlayerState
{
    public override PlayerState State => PlayerState.Casting;

    public override void StartState(Player player)
    {
        TooltipsController.Instance.TooltipWindowController.ClearTooltipWindowBeforeClosing();
    }

    public override void EndState(Player player)
    {
    }

    public override void RunUpdate(Player player, GameplayController gameplayController)
    {
        if (InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.EndTurnOrCancelAction.WasReleasedThisFrame())
        {
            EndCasting();
        }
        else if (InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainAction.WasReleasedThisFrame())
        {
            TryCastSpell(InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>(), Player.Instance.Attributes[AttributeID.PlayerModifierSpellDamage].CurrentValue,
                Player.Instance.Attributes[AttributeID.PlayerModifierSpellRange].CurrentValue, Player.Instance.Attributes[AttributeID.PlayerModifierSpellEffectDuration].CurrentValue);
        }


        TilesHighlighterTilesInRange.Instance.UpdateHighlightedTiles(SpellsController.Instance.CurrentSpell.ReturnTilesInRange(Map.Instance.MapData, 
            Player.Instance.Attributes[AttributeID.PlayerModifierSpellRange].CurrentValue));
        TilesHighlighterSelectedTiles.Instance.UpdateHighlightedTiles(SpellsController.Instance.CurrentSpell.ReturnAffectedTilesPositions(Map.Instance.MapData, 
            InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>(), Player.Instance.Attributes[AttributeID.PlayerModifierSpellRange].CurrentValue));
    }

    private void TryCastSpell(Vector2 cursorPos, int modifierDamage, int modifierRange, int modifierEffectLength)
    {
        if(!CheckIfObscuredByUI.Instance.CheckIfObscured())
        {
            if (SpellsController.Instance.CurrentSpell.TryCast(Map.Instance.MapData, InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>(),
    modifierDamage, modifierRange, modifierEffectLength))
            {
                WrapUpAfterSuccesfullSpell(Map.Instance.MapData, InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>(),
                    SpellsController.Instance.CurrentSpell, modifierRange);
            }
            else
            {
                EndCasting();
            }
        }
    }

    private void EndCasting()
    {
        //wy³¹cz podœwietlenie
        Player.Instance.SwitchState(PlayerState.Idle);
    }

    private void WrapUpAfterSuccesfullSpell(Tile[,] mapData, Vector2 cursorPos, BaseSpell spell, int modifierRange)
    {
        Player.Instance.SetSpellToSpentThisTurn();
        VFXController.Instance.Play(spell.SpellVFX, SpellsController.Instance.CurrentSpell.ReturnAffectedTilesPositions(mapData, 
            cursorPos, modifierRange));
        EnemiesDisplayControler.Instance.Display(EnemiesController.Instance.EnemiesOnMap);
        EnemiesNormalAtributesUIDisplayController.Instance.UpdateUI(Map.Instance.MapData);

        EndCasting();
    }
}
