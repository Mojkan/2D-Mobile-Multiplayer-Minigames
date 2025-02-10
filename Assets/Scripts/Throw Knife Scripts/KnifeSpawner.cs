using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float ySpawnPos;

    [Header("Reference")]
    [SerializeField] GameManager gameManager;
    [SerializeField] ObjectPool knifeObjectPool;

    float knifeCount;

    public void SpawnNewKnife()
    {
        if (gameManager.currentGameState == GameManager.GameState.running)
        {
            InstantiateKnife();
        }
    }

    void InstantiateKnife()
    {
        Vector2 knifeSpawnPos = new Vector2(0, ySpawnPos);

        GameObject newKnife = knifeObjectPool.GetPrefab();
        newKnife.transform.position = knifeSpawnPos;
        /*
        newKnife.GetComponent<KnifeDestroy>().knifeObjectPool = knifeObjectPool;
        newKnife.GetComponent<KnifeMovement>().gameManager = gameManager;
        newKnife.GetComponent<KnifeMovement>().knifeSpawner = this;*/
    }
}
