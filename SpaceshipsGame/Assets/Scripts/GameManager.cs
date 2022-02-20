using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    internal static GameManager Instance;

    [SerializeField]
    private AudioSource sfx;

    [SerializeField]
    private MusicControl music;

    [SerializeField]
    private Text scoreLabel;

    [SerializeField]
    private GameObject gameOver;

    [SerializeField]
    private Button restartButton;

    private int score;

    internal void PlaySfx(AudioClip clip) => sfx.PlayOneShot(clip);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        score = 0;
        scoreLabel.text = $"Score: {score}";
        gameOver.gameObject.SetActive(false);

        restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1f;
        });
        restartButton.gameObject.SetActive(false);
    }

    internal void UpdateScore(int value)
    {
        score += value;
        scoreLabel.text = $"Score: {score}";
    }

    internal void TriggerGameOver(bool failure = true)
    {
        gameOver.SetActive(failure);
        restartButton.gameObject.SetActive(true);
        GetComponent<InvaderSwarm>().enabled = false;
        Time.timeScale = 0f;
        music.StopPlaying();
    }
   

}
