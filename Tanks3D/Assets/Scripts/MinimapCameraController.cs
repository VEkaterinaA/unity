using UnityEngine;
using Zenject;

public class MinimapCameraController
{
    private Camera MinimapCamera;

    private MinimapCameraController(Camera minimapCamera)
    {
        MinimapCamera = minimapCamera;
    }

    public void ChangeMiniMapCameraPosition(Transform playerController)
    {
        MinimapCamera.transform.position = new Vector3(playerController.position.x, 15f, playerController.position.z);
        MinimapCamera.transform.rotation = Quaternion.Euler(90, 0, playerController.rotation.z);
    }
    public void StartMiniMapCameraPosition(Transform playerController)
    {
        MinimapCamera.transform.SetParent(playerController);
        MinimapCamera.transform.localPosition = new Vector3(0, 15, 0);
    }
}
