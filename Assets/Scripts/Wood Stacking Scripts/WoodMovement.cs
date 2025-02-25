using UnityEngine;

public class WoodMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float moveSpeedHorizontal;

    [Header("Reference")]
    [SerializeField] Rigidbody2D rb2D;
    [HideInInspector] public WoodSpawner woodSpawner;
    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public bool woodDropped;

    void Start()
    {
        moveSpeedHorizontal = Random.Range(0f, 1f) > 0.5f ? moveSpeedHorizontal : -moveSpeedHorizontal;
    }

    void Update()
    {
        if (!woodDropped && gameManager.currentGameState == GameManager.GameState.end)
        {
            Destroy(gameObject);
        }

        CheckInput();
        AddMovement();
        CheckWoodLanding();
    }

    void CheckInput()
    {
        if (!woodDropped && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            woodDropped = true;
            gameObject.tag = "ScoreObject";
        }

        if (!woodDropped && Input.GetMouseButtonDown(0))
        {
            woodDropped = true;
            gameObject.tag = "ScoreObject";
        }
    }

    void AddMovement()
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
        else
        {
            rb2D.gravityScale = 1;
        }
    }

    void CheckWoodLanding()
    {
        if (woodDropped)
        {
            RaycastHit2D middleDown = Physics2D.Raycast(transform.position + new Vector3(0, -0.2505f, 0), Vector2.down, 0.01f, LayerMask.GetMask("Ground"));
            RaycastHit2D rightDown = Physics2D.Raycast(transform.position + new Vector3(0.25f, -0.2505f, 0), Vector2.down, 0.01f, LayerMask.GetMask("Ground"));
            RaycastHit2D leftDown = Physics2D.Raycast(transform.position + new Vector3(-0.25f, -0.2505f, 0), Vector2.down, 0.01f, LayerMask.GetMask("Ground"));

            if (middleDown.collider != null || rightDown.collider != null || leftDown.collider != null)
            {
                SoundManager.Instance.PlaySound("WOODLANDING");
                woodSpawner.SpawnNewWood();
                this.enabled = false;
            }
        }
    }

    public void ResetWood()
    {
        this.enabled = true;
        rb2D.gravityScale = 0;
        transform.rotation = Quaternion.Euler(0, 0, 90);
        woodDropped = false;
    }
}
