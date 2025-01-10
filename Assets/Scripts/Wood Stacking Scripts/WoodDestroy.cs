using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodDestroy : MonoBehaviour
{
    [SerializeField] float destroyHeight;
    WoodSpawner woodSpawner;

    void Start()
    {
        woodSpawner = GameObject.Find("WoodSpawner").GetComponent<WoodSpawner>();
    }

    void Update()
    {
        if (transform.position.y < destroyHeight)
        {
            woodSpawner.UpdateWoodManager();
            Destroy(gameObject);
        }
    }
}
