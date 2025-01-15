using UnityEngine;

public class WoodMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float moveSpeedHorizontal;
    [SerializeField] float gravityStrength;

    [Header("Reference")]
    [SerializeField] Rigidbody2D rb2D;
    WoodSpawner woodSpawner;
    GameManager gameManager;

    float fallSpeed;
    bool woodDropped;

    void Start()
    {
        woodSpawner = GameObject.Find("WoodSpawner").GetComponent<WoodSpawner>();    
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        moveSpeedHorizontal = Random.Range(0f, 1f) > 0.5f ? moveSpeedHorizontal : -moveSpeedHorizontal;
    }

    void Update()
    {
        if (gameManager.currentGameState != GameManager.GameState.running && !woodDropped)
        {
            Destroy(gameObject);
        }

        GetInput();
        AddHorizontalMovment();
        AddVerticalMovment();
        CheckWoodLanding();
    }

    void GetInput()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            woodDropped = true;
            woodSpawner.currentWood = null;
        }

        if (Input.GetMouseButtonDown(0))
        {
            woodDropped = true;
            woodSpawner.currentWood = null;
        }
    }

    void AddHorizontalMovment()
    {
        if (!woodDropped)
        {
            if (transform.position.x >= 1.5f)
            {
                moveSpeedHorizontal = -Mathf.Abs(moveSpeedHorizontal);
            }
            else if (transform.position.x <= -1.5f)
            {
                moveSpeedHorizontal = Mathf.Abs(moveSpeedHorizontal);
            }

            rb2D.transform.position += new Vector3(moveSpeedHorizontal * Time.deltaTime, 0, 0);
        }
    }

    void AddVerticalMovment()
    {
        if (woodDropped)
        {
            fallSpeed += gravityStrength * Time.deltaTime;

            rb2D.transform.position += new Vector3(0, -fallSpeed * Time.deltaTime, 0);
        }
    }

    void CheckWoodLanding()
    {
        if (woodDropped)
        {
            RaycastHit2D hit1 = Physics2D.Raycast(transform.position + new Vector3(0, -0.2505f, 0), Vector2.down, fallSpeed * Time.deltaTime, LayerMask.GetMask("Ground"));
            RaycastHit2D hit2 = Physics2D.Raycast(transform.position + new Vector3(0.25f, -0.2505f, 0), Vector2.down, fallSpeed * Time.deltaTime, LayerMask.GetMask("Ground"));
            RaycastHit2D hit3 = Physics2D.Raycast(transform.position + new Vector3(-0.25f, -0.2505f, 0), Vector2.down, fallSpeed * Time.deltaTime, LayerMask.GetMask("Ground"));

            if (hit1.collider != null || hit2.collider != null || hit3.collider != null)
            {
                rb2D.velocity = Vector2.zero;
                rb2D.gravityScale = 1;

                woodSpawner.SpawnNewWood();
                this.enabled = false;
            }
        }
    }
}
