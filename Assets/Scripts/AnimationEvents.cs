using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] WoodSpawner woodSpawner;

    void startGame()
    {
        gameManager.currentState = GameManager.GameState.running;
    }

    void startWoodSpawner()
    {
        woodSpawner.SpawnNewWood();
    }
}
