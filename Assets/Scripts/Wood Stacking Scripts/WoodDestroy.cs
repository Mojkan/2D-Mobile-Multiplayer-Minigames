using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodDestroy : MonoBehaviour
{
    [SerializeField] WoodMovement woodMovement;
    [SerializeField] float destroyHeight;
    [HideInInspector] public ObjectPool woodObjectPool;

    void Update()
    {
        if (transform.position.y < destroyHeight)
        {
            woodMovement.ResetWood();
            woodObjectPool.ReturnPrefab(gameObject);
        }
    }
}
