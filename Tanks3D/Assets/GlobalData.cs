using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalData
{
    public static int CountScene=0;

    GlobalData()
    {
        CountScene = SceneManager.sceneCountInBuildSettings;
    }
}
