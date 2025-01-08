using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float minXSpawnPos;
    [SerializeField] float maxXSpawnPos;
    [SerializeField] public float ySpawnPos;

    [Header("Reference")]
    [SerializeField] GameObject woodPrefab;

    [HideInInspector] public GameObject currentWood;

    void Update()
    {
        if (currentWood == null)
        {
            Vector2 spawnPos = new Vector2(Random.Range(minXSpawnPos, maxXSpawnPos), ySpawnPos);
            currentWood = Instantiate(woodPrefab, spawnPos, woodPrefab.transform.rotation);
        }    
    }
}
