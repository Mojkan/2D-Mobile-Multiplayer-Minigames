using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float gameTimeLimit;
    [SerializeField] float maxTimeCounting;

    [Header("Reference")]
    [SerializeField] GameObject FadeOutUI;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI scoreText;

    float gameTimer;
    int points;

    [HideInInspector] public enum GameState
    {
        start,
        running,
        end
    }

    public GameState currentGameState = GameState.start;

    void Start()
    {
        Physics2D.gravity = new Vector3(0, -9.81f, 0); // Fixes gravity bug when replaying the game
        gameTimer = gameTimeLimit;
    }

    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        if (currentGameState == GameState.running)
        {
            if (gameTimer > 0)
            {
                timerText.text = gameTimer.ToString("F1");
                gameTimer -= Time.deltaTime;
            }
            else
            {
                timerText.gameObject.SetActive(false);
                currentGameState = GameState.end;
                StartCoroutine(CountScore());
            }
        }
    }

    private IEnumerator CountScore()
    {
        Physics2D.gravity = Vector2.zero;

        GameObject[] scoreGameObjects = GameObject.FindGameObjectsWithTag("ScoreObject");

        GameLobbyManager.Instance.UploadScore(scoreGameObjects.Length);

        float countSpeed = maxTimeCounting / scoreGameObjects.Length;

        if (scoreGameObjects.Length != 0)
        {
            for (int i = 0; i < scoreGameObjects.Length; i++)
            {
                Destroy(scoreGameObjects[i]);
                UpdateScore();
                SoundManager.Instance.PlaySoundWithPitchIncrease("SCORECOUNTING");
                yield return new WaitForSeconds(countSpeed);
            }

            GameEnd();
        }
        else
        {
            Invoke(nameof(GameEnd), maxTimeCounting);
        }
    }

    void UpdateScore()
    {
        points++;
        scoreText.text = points.ToString();
    }

    void GameEnd()
    {
        GameLobbyManager.Instance.isGameCompleted = true;
        FadeOutUI.SetActive(true);
        Invoke(nameof(LoadMenuScene), 2);
    }

    void LoadMenuScene()
    {
        if (SceneManager.GetActiveScene().name == "WoodStacking")
        {
            SceneManager.LoadScene("ThrowKnife");
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
