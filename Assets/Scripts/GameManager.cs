using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public enum GameState
    {
        start,
        running,
        end
    }

    public GameState currentState = GameState.start;

    void Update()
    {
        
    }
}
