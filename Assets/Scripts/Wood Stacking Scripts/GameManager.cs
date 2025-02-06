using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float gameTimeLimit;
    [SerializeField] float woodCountSpeed;

    [Header("Reference")]
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
        Physics2D.gravity = new Vector3(0, -9.81f, 0); // Fixes no gravity bug when replaying the game
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

        GameObject[] wood = GameObject.FindGameObjectsWithTag("Wood");
        for (int i = 0; i < wood.Length; i++)
        {
            Destroy(wood[i]);
            UpdateScore();
            yield return new WaitForSeconds(woodCountSpeed);
        }

        SendScoreToFirebase();
    }

    void UpdateScore()
    {
        points++;
        scoreText.text = points.ToString();
    }

    void SendScoreToFirebase()
    {
        FirebaseManager.Instance.UpdatePlayerScore(points, SendScoreToFirebaseOnSuccess);
    }

    void SendScoreToFirebaseOnSuccess()
    {
        SceneManager.LoadScene("Menu");
    }
}
