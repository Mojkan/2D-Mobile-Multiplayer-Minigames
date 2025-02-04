using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodDestroy : MonoBehaviour
{
    [SerializeField] WoodMovement woodMovement;
    [SerializeField] float destroyHeight;
    WoodObjectPool woodObjectPool;

    void Start()
    {
        woodObjectPool = GameObject.Find("WoodObjectPool").GetComponent<WoodObjectPool>();
    }

    void Update()
    {
        if (transform.position.y < destroyHeight)
        {
            woodMovement.ResetWood();
            woodObjectPool.ReturnWood(gameObject);
        }
    }
}
