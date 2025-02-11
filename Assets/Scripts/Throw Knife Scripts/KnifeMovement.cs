using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float shootSpeed;
    [SerializeField] float destroyShootSpeed;
    [SerializeField] float minDestroyDistance;

    [Header("Reference")]
    [SerializeField] Rigidbody2D rb2D;
    [SerializeField] BoxCollider2D collider2D;
    [HideInInspector] public Transform woodTarget;
    [HideInInspector] public KnifeSpawner knifeSpawner;
    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public ObjectPool knifeObjectPool;

    [HideInInspector] public bool knifeDropped;
    [HideInInspector] public bool knifeCollided;
    [HideInInspector] public bool knifeDestroy;

    Vector3 knifeDirection;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ScoreObject") && !knifeCollided)
        {
            Debug.Log("Knife");
            ResetKnife();
            knifeSpawner.DestroyAllKnifes();
            knifeSpawner.SpawnNewKnife();
        }
        else if (collision.gameObject.CompareTag("WoodTarget"))
        {
            Debug.Log("Wood");
            knifeCollided = true;
            knifeSpawner.SpawnNewKnife();
            transform.SetParent(collision.transform);
        }
    }

    void Update()
    {
        if (!knifeDropped && gameManager.currentGameState == GameManager.GameState.end)
        {
            Destroy(gameObject);
        }

        CheckInput();
        AddMovement();
    }

    void CheckInput()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            knifeDropped = true;
            gameObject.tag = "ScoreObject";
        }

        if (Input.GetMouseButtonDown(0))
        {
            knifeDropped = true;
            gameObject.tag = "ScoreObject";
        }
    }

    void AddMovement()
    {
        if (!knifeCollided && knifeDropped)
        {
            rb2D.transform.position += new Vector3(0, shootSpeed * Time.deltaTime, 0);
        }

        if (knifeDestroy)
        {
            if (Vector2.Distance(woodTarget.position, transform.position) < minDestroyDistance)
            {
                rb2D.transform.position -= knifeDirection * destroyShootSpeed * Time.deltaTime;
            }
            else
            {
                ResetKnife();
            }
        }
    }

    public void DestroyKnife()
    {
        gameObject.tag = "Untagged";
        transform.SetParent(null);
        collider2D.enabled = false;
        knifeDestroy = true;

        knifeDirection = woodTarget.position - transform.position;
        knifeDirection.Normalize();
    }

    void ResetKnife()
    {
        knifeObjectPool.ReturnPrefab(gameObject);
        gameObject.tag = "ScoreObject";
        transform.rotation = Quaternion.Euler(Vector3.zero);

        collider2D.enabled = true;
        transform.SetParent(null);
        knifeCollided = false;
        knifeDropped = false;
        knifeDestroy = false;
    }
}
