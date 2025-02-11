using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float ySpawnPos;
    [SerializeField] float woodTargetRotateSpeed;

    [Header("Reference")]
    [SerializeField] GameManager gameManager;
    [SerializeField] ObjectPool knifeObjectPool;
    [SerializeField] Transform WoodTarget;

    void Update()
    {
        RotateWoodTarget();
    }

    void RotateWoodTarget()
    {
        if (gameManager.currentGameState == GameManager.GameState.running)
        {
            WoodTarget.Rotate(0, 0, woodTargetRotateSpeed * Time.deltaTime);
        }
    }

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
        KnifeMovement knifeMovment = newKnife.GetComponent<KnifeMovement>();

        knifeMovment.gameManager = gameManager;
        knifeMovment.knifeSpawner = this;
        knifeMovment.knifeObjectPool = knifeObjectPool;
        knifeMovment.woodTarget = WoodTarget;
    }

    public void DestroyAllKnifes()
    {
        GameObject[] knifes = GameObject.FindGameObjectsWithTag("ScoreObject");

        foreach(var knife in knifes)
        {
            knife.GetComponent<KnifeMovement>().DestroyKnife();
        }
    }
}
