using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float moveSpeed;
    [SerializeField] float gravity;

    [Header("Reference")]
    [SerializeField] Rigidbody2D rb2D;

    WoodSpawner woodSpawner;
    float fallSpeed;
    bool woodDropped;

    void Start()
    {
        woodSpawner = GameObject.Find("WoodSpawner").GetComponent<WoodSpawner>();    
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            woodDropped = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            woodDropped = true;
        }

        AddMovmentXAxis();
        AddMovementYAxis();
        CheckWoodLanding();
    }

    void AddMovmentXAxis()
    {
        if (!woodDropped)
        {
            if (transform.position.x >= 1.5f)
            {
                moveSpeed = -Mathf.Abs(moveSpeed);
            }
            else if (transform.position.x <= -1.5f)
            {
                moveSpeed = Mathf.Abs(moveSpeed);
            }

            rb2D.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        }
    }

    void AddMovementYAxis()
    {
        if (woodDropped)
        {
            fallSpeed += gravity * Time.deltaTime;

            rb2D.transform.position += new Vector3(0, -fallSpeed * Time.deltaTime, 0);
        }
    }

    void CheckWoodLanding()
    {
        if (woodDropped)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, -0.251f, 0), Vector2.down, fallSpeed * Time.deltaTime, LayerMask.GetMask("Ground"));

            if (hit.collider != null)
            {
                rb2D.velocity = Vector2.zero;
                rb2D.gravityScale = 1;

                woodSpawner.ySpawnPos += transform.localScale.x;
                woodSpawner.currentWood = null;
                this.enabled = false;
            }
        }
    }
}
