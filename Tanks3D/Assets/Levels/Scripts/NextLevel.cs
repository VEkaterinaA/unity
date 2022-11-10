using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            int IdNexnScene = SceneManager.GetActiveScene().buildIndex + 1;
            if (IdNexnScene < GlobalData.CountScene)
            {
                SceneTransition.SwitchToScene(IdNexnScene);

            }
        }
    }
}
