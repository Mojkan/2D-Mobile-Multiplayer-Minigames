using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float timeLimit;

    [Header("Reference")]
    [SerializeField] TextMeshProUGUI timerText;

    float timer;

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
            }
        }
    }
}
