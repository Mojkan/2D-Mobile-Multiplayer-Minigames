using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float timeLimit;
    [SerializeField] float maxScoreColor;

    [Header("Reference")]
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI scoreText;

    float timer;
    int points;

    [HideInInspector] public enum GameState
    {
        start,
        running,
        end
    }

    public GameState currentState = GameState.start;

    void Start()
    {
        timer = timeLimit;
    }

    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        if (currentState == GameState.running)
        {
            if (timer > 0)
            {
                timerText.text = timer.ToString("F1");
                timer -= Time.deltaTime;
            }
            else
            {
                timerText.gameObject.SetActive(false);
                currentState = GameState.end;
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
            yield return new WaitForSeconds(0.2f);
        }
    }

    void UpdateScore()
    {
        points++;
        scoreText.text = points.ToString();

        float value = points / maxScoreColor;
        scoreText.color = Color.Lerp(Color.green, Color.red, value);
    }
}
