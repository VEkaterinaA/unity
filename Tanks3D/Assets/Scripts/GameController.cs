using Assets.Scripts.Player;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    [Inject]
    private ScoreText scoreText;
  
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("Score", scoreText._Score);
        PlayerPrefs.Save();
    }
}
