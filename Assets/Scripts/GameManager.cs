using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float gameTimeLimit;
    [SerializeField] float maxScoreColor;
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
                StartCoroutine(CountWood());
            }
        }
    }

    private IEnumerator CountWood()
    {
        Physics2D.gravity = Vector2.zero;
        GameObject[] wood = GameObject.FindGameObjectsWithTag("Wood");

        for (int i = 0; i < wood.Length; i++)
        {
            Destroy(wood[i]);
            UpdateScore();
            yield return new WaitForSeconds(woodCountSpeed);
        }
    }

    void UpdateScore()
    {
        points++;
        scoreText.text = points.ToString();

        float value = points / maxScoreColor;
        scoreText.color = Color.Lerp(Color.white, Color.red, value);
    }
}
