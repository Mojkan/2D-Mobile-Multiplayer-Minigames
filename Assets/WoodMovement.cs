using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float moveSpeed;

    [Header("Reference")]
    [SerializeField] Rigidbody2D rb2D;

    WoodSpawner woodSpawner;
    bool woodDropped;
    bool checkLanding;

    void Start()
    {
        woodSpawner = GameObject.Find("WoodSpawner").GetComponent<WoodSpawner>();    
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            woodDropped = true;
            rb2D.gravityScale = 1;
            Invoke(nameof(DroppedWood), 0.2f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            woodDropped = true;
            rb2D.gravityScale = 1;
            Invoke(nameof(DroppedWood), 0.2f);
        }

        AddMovment();
        CheckWoodLanding();
    }

    void AddMovment()
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

    void DroppedWood()
    {
        checkLanding = true;
    }

    void CheckWoodLanding()
    {
        if (checkLanding)
        {
            if (rb2D.velocity.magnitude < 0.01f || transform.position.y < -6)
            {
                woodSpawner.ySpawnPos += transform.localScale.x;
                woodSpawner.currentWood = null;
                this.enabled = false;
            }
        }
    }
}
