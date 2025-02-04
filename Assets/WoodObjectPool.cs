using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodObjectPool : MonoBehaviour
{
    [SerializeField] GameObject woodPrefab;
    [SerializeField] int woodPoolSize = 25;

    private Queue<GameObject> woodPool = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < woodPoolSize; i++)
        {
            GameObject wood = Instantiate(woodPrefab);
            wood.SetActive(false);
            woodPool.Enqueue(wood);
        }
    }

    public GameObject GetWood()
    {
        if (woodPool.Count > 0)
        {
            GameObject wood = woodPool.Dequeue();
            wood.SetActive(true);
            return wood;
        }
        else
        {
            GameObject newWood = Instantiate(woodPrefab);
            return newWood;
        }
    }

    public void ReturnWood(GameObject wood)
    {
        wood.SetActive(false);
        woodPool.Enqueue(wood);
    }
}
