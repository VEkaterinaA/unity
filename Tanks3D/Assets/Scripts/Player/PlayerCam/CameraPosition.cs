using UnityEngine;

public class CameraPosition
    {
    public void ChangeCameraPosition(
       ClassCameraViewEnum.CameraViewTypes cameraView,
        Camera FirstCameraPlayer,
        Camera SecondCameraPlayer,
        Camera ThirdCameraPlayer
)
    {
        switch (cameraView)
        {
            case ClassCameraViewEnum.CameraViewTypes.First:
                FirstCameraPlayer.enabled = true;
                SecondCameraPlayer.enabled = false;
                ThirdCameraPlayer.enabled = false;
                break;
            case ClassCameraViewEnum.CameraViewTypes.Second:
                FirstCameraPlayer.enabled = false;
                SecondCameraPlayer.enabled = true;
                ThirdCameraPlayer.enabled = false;
                break;
            default:
                FirstCameraPlayer.enabled = false;
                SecondCameraPlayer.enabled = false;
                ThirdCameraPlayer.enabled = true;
                break;
        }
    }
    
}
