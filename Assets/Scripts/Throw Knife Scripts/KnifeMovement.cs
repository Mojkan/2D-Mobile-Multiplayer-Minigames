using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float shootSpeed;

    [Header("Reference")]
    [SerializeField] Rigidbody2D rb2D;
    [HideInInspector] public KnifeSpawner knifeSpawner;
    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public bool knifeDropped;
    [HideInInspector] public bool knifeCollided;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ScoreObject"))
        {
            DestroyKnife();
        }
        else
        {
            knifeCollided = true;
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

    void DestroyKnife()
    {

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
    }
}
