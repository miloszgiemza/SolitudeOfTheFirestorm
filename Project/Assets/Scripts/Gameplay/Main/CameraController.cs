using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 cameraStartingPosition = new Vector3(6.915993f, 12.91165f, -28.8382f);

    private Camera currentCamera;

    private float speedZoomMouseScroll = 1f;
    private float speedZoomTouchscreen = 1.7f;
    private float speedMovementTouchscreen = 0.4f;
    private float speedMovementWSAD = 10f;

    private void Awake()
    {
        currentCamera = Camera.main;
        currentCamera.transform.position = cameraStartingPosition;
    }

    private void Update()
    {
        Zoom();
        Move();
        MoveSwipe(SwipeController.Instance.Swiping, SwipeController.Instance.SwipeLastFrameValue);
    }

    private void Zoom()
    {
        if(InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.Zoom.ReadValue<float>()!=0)
        {
            currentCamera.transform.position = new Vector3(currentCamera.transform.position.x, currentCamera.transform.position.y, currentCamera.transform.position.z + (InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.Zoom.ReadValue<float>() * speedZoomMouseScroll * Time.deltaTime));
        }

        if(PinchController.Instance.Pinching)
        {
            currentCamera.transform.position = new Vector3(currentCamera.transform.position.x, currentCamera.transform.position.y, currentCamera.transform.position.z + ( (float) PinchController.Instance.PinchValueTwoFrames * speedZoomTouchscreen * Time.deltaTime));
        }
    }

    private void Move()
    {
        if(InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.CameraMovement.ReadValue<Vector2>().x != 0 
            || InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.CameraMovement.ReadValue<Vector2>().y != 0)
        {
            currentCamera.transform.position = new Vector3(
                currentCamera.transform.position.x + (InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.CameraMovement.ReadValue<Vector2>().x * Time.deltaTime * speedMovementWSAD),
                currentCamera.transform.position.y + (InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.CameraMovement.ReadValue<Vector2>().y * Time.deltaTime * speedMovementWSAD),
                currentCamera.transform.position.z);
        }
    }

    private void MoveSwipe(bool swiping, Vector2 movement)
    {
        if(Player.Instance.CurrentState.State == PlayerState.Idle)
        {
            if (swiping)
            {
                currentCamera.transform.position = new Vector3(currentCamera.transform.position.x + (movement.x * Time.deltaTime * speedMovementTouchscreen),
                currentCamera.transform.position.y + (movement.y * Time.deltaTime * speedMovementTouchscreen), currentCamera.transform.position.z);
            }
        }
    }
}
