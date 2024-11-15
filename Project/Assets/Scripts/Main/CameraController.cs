using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera currentCamera;

    private float speedZoom = 1f;
    private float speedMovement = 10f;

    private void Awake()
    {
        currentCamera = Camera.main;
    }

    private void Update()
    {
        Zoom();
        Move();
    }

    private void Zoom()
    {
        if(InputControllerGameplay.Instance.MainInputAssetsWrapper.MobileDevicesMap.Zoom.ReadValue<float>()!=0)
        {
            currentCamera.transform.position = new Vector3(currentCamera.transform.position.x, currentCamera.transform.position.y, currentCamera.transform.position.z + (InputControllerGameplay.Instance.MainInputAssetsWrapper.MobileDevicesMap.Zoom.ReadValue<float>() * speedZoom * Time.deltaTime));
        }
    }

    private void Move()
    {
        if(InputControllerGameplay.Instance.MainInputAssetsWrapper.MobileDevicesMap.CameraMovement.ReadValue<Vector2>().x != 0 
            || InputControllerGameplay.Instance.MainInputAssetsWrapper.MobileDevicesMap.CameraMovement.ReadValue<Vector2>().y != 0)
        {
            currentCamera.transform.position = new Vector3(
                currentCamera.transform.position.x + (InputControllerGameplay.Instance.MainInputAssetsWrapper.MobileDevicesMap.CameraMovement.ReadValue<Vector2>().x * Time.deltaTime * speedMovement),
                currentCamera.transform.position.y + (InputControllerGameplay.Instance.MainInputAssetsWrapper.MobileDevicesMap.CameraMovement.ReadValue<Vector2>().y * Time.deltaTime * speedMovement),
                currentCamera.transform.position.z);
        }
    }
}
