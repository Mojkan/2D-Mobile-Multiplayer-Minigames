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
    [SerializeField] BoxCollider2D knifeCollider;
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
            SoundManager.Instance.PlaySound("KNIFEFAIL");
            knifeCollided = true;
            knifeSpawner.DestroyAllActiveKnifes();
        }
        else if (collision.gameObject.CompareTag("WoodTarget") && !knifeCollided)
        {
            SoundManager.Instance.PlaySound("KNIFEHIT");
            knifeCollided = true;
            transform.SetParent(collision.transform);
            knifeSpawner.SpawnNewKnife();
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
        if (!knifeDropped && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            knifeDropped = true;
            gameObject.tag = "ScoreObject";
        }

        if (!knifeDropped && Input.GetMouseButtonDown(0))
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
                transform.position -= knifeDirection * destroyShootSpeed * Time.deltaTime;
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
        knifeCollider.enabled = false;
        transform.SetParent(null);

        knifeDirection = woodTarget.position - transform.position;
        knifeDirection.Normalize();
        knifeDestroy = true;
    }

    void ResetKnife()
    {
        knifeSpawner.DestroyAllKnivesEvent -= DestroyKnife;

        transform.rotation = Quaternion.Euler(Vector3.zero);

        knifeCollider.enabled = true;

        knifeCollided = false;
        knifeDropped = false;
        knifeDestroy = false;

        knifeObjectPool.ReturnPrefab(gameObject);
    }
}
