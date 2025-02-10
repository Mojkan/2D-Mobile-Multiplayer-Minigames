using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int poolSize = 25;

    private Queue<GameObject> prefabPool = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject prefab = Instantiate(this.prefab);
            prefab.SetActive(false);
            prefabPool.Enqueue(prefab);
        }
    }

    public GameObject GetPrefab()
    {
        if (prefabPool.Count > 0)
        {
            GameObject prefab = prefabPool.Dequeue();
            prefab.SetActive(true);
            return prefab;
        }
        else
        {
            GameObject newPrefab = Instantiate(prefab);
            return newPrefab;
        }
    }

    public void ReturnPrefab(GameObject prefab)
    {
        prefab.SetActive(false);
        prefabPool.Enqueue(prefab);
    }
}
