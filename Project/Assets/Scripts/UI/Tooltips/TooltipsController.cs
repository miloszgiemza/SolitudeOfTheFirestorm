using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipsController : MonoBehaviour
{
    public static TooltipsController Instance => instance;
    protected static TooltipsController instance;

    public TooltipWindowController TooltipWindowController => tooltipWindowController;

    [SerializeField] protected TooltipWindowController tooltipWindowController;
    [SerializeField] protected TooltipValidObjectsDetector tooltipValidObjectsDetectorScreenSpace;
    [SerializeField] protected TooltipValidObjectsDetector tooltipValidObjectsDetectorWorldSpace;

    protected void Awake()
    {
        if(ReferenceEquals(TooltipsController.Instance, null))
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    protected void OnDestroy()
    {
        instance = null;
    }

    protected void Update()
    {

            if (InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainAction.IsPressed())
            {
                if (ReferenceEquals(Player.Instance, null) || (!ReferenceEquals(Player.Instance, null) && (Player.Instance.CurrentState.State == PlayerState.Idle || Player.Instance.CurrentState.State == PlayerState.Deactivated)) )
                {

                    if(!CheckIfObscuredByRaycastBLocker.Instance.CheckIfObscured())
                    {
                        CheckForGameObject(InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>());
                        if (!ReferenceEquals(Map.Instance, null)) CheckForLogicalObjectOnMapField(InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>(), Map.Instance.MapData);
                    }
                }
            }
        else
        {
            tooltipWindowController.ClearTooltipWindowBeforeClosing();
        }
    }

    protected  void CheckForLogicalObjectOnMapField(Vector2 cursorPos, Tile[,] mapData)
    {
        if(!CheckIfObscuredByUI.Instance.CheckIfObscured())
        {
            if (GameWorldToMapCastController.Instance.CastGameWorldPosToMap(InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>()).X >= 0)
            {
                MapPosition cursorMapPos = GameWorldToMapCastController.Instance.CastGameWorldPosToMap(cursorPos);

                if (cursorMapPos.X > 0 && cursorMapPos.X < mapData.GetLength(0) && cursorMapPos.Y > 0 && cursorMapPos.Y < mapData.GetLength(1))
                {
                    if (!ReferenceEquals(mapData[cursorMapPos.X, cursorMapPos.Y].EnemySocket, null) /*&& mapData[cursorMapPos.X, cursorMapPos.Y].EnemySocket.ReturnTooltipText(GameController.Instance.GameLanguage).Length > 0*/)
                    {
                        tooltipWindowController.ShowTooltip(mapData[cursorMapPos.X, cursorMapPos.Y].EnemySocket.ReturnTooltipText(GameController.Instance.GameLanguage),
                            InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>());
                    }
                    else if (!ReferenceEquals(mapData[cursorMapPos.X, cursorMapPos.Y].ObstacleSocket, null) /*&& mapData[cursorMapPos.X, cursorMapPos.Y].ObstacleSocket.ReturnTooltipText(GameController.Instance.GameLanguage).Length > 0*/)
                    {
                        tooltipWindowController.ShowTooltip(mapData[cursorMapPos.X, cursorMapPos.Y].ObstacleSocket.ReturnTooltipText(GameController.Instance.GameLanguage),
                             InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>());
                    }
                    else
                    {
                        tooltipWindowController.ClearTooltipWindowBeforeClosing();
                    }
                }
            }
        }
    }

    protected virtual void CheckForGameObject(Vector2 cursorPos)
    {
        if(tooltipValidObjectsDetectorScreenSpace.CheckForObjects())
        {
            tooltipWindowController.ShowTooltip(tooltipValidObjectsDetectorScreenSpace.ObjectDataForTooltip.ReturnTooltipText(GameController.Instance.GameLanguage), cursorPos);
        }
        else if(tooltipValidObjectsDetectorWorldSpace.CheckForObjects())
        {
            tooltipWindowController.ShowTooltip(tooltipValidObjectsDetectorWorldSpace.ObjectDataForTooltip.ReturnTooltipText(GameController.Instance.GameLanguage), cursorPos);
        }
        else
        {
            tooltipWindowController.ClearTooltipWindowBeforeClosing();
        }
    }
}
