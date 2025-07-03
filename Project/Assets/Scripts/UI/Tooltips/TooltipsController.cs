using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipsController : MonoBehaviour
{
    public static TooltipsController Instance => instance;
    private static TooltipsController instance;

    public TooltipWindowController TooltipWindowController => tooltipWindowController;

    [SerializeField] private TooltipWindowController tooltipWindowController;
    [SerializeField] private TooltipValidObjectsDetector tooltipValidObjectsDetectorScreenSpace;
    [SerializeField] private TooltipValidObjectsDetector tooltipValidObjectsDetectorWorldSpace;

    private Vector2 previousFrameCoursorPos;

    private void Awake()
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

    private void OnDestroy()
    {
        instance = null;
    }

    private void Update()
    {
        if(!ReferenceEquals(previousFrameCoursorPos, null))
        {
            if (InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>().x != previousFrameCoursorPos.x
                || InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>().y != previousFrameCoursorPos.y)
            {
                if (Player.Instance.CurrentState.State == PlayerState.Idle || Player.Instance.CurrentState.State == PlayerState.Deactivated)
                {
                    CheckForLogicalObjectOnMapField(InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>(), Map.Instance.MapData);

                    CheckForGameObject(InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>());
                }
            }

            previousFrameCoursorPos = new Vector2(InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>().x,
                InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>().y);
        }
        else
        {
            previousFrameCoursorPos = new Vector2(InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>().x,
                InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>().y);
        }
    }

    private void CheckForLogicalObjectOnMapField(Vector2 cursorPos, Tile[,] mapData)
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

    private void CheckForGameObject(Vector2 cursorPos)
    {
        if(tooltipValidObjectsDetectorScreenSpace.CheckForObjects())
        {
            tooltipWindowController.ShowTooltip(tooltipValidObjectsDetectorScreenSpace.ObjectDataForTooltip.ReturnTooltipText(GameController.Instance.GameLanguage), cursorPos);
        }
        if(tooltipValidObjectsDetectorWorldSpace.CheckForObjects())
        {
            tooltipWindowController.ShowTooltip(tooltipValidObjectsDetectorWorldSpace.ObjectDataForTooltip.ReturnTooltipText(GameController.Instance.GameLanguage), cursorPos);
        }
    }
}
