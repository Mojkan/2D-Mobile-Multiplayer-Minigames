using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    [SerializeField] WoodSpawner woodSpawner;
    //[SerializeField] KnifeSpawner knifeSpawner;

    public void startGame()
    {
        gameManager.currentGameState = GameManager.GameState.running;

        if (woodSpawner != null)
        {
            woodSpawner.SpawnNewWood();
        }
        else
        {
            //knifeSpawner.SpawnNewKnife();
        }
    }
}
