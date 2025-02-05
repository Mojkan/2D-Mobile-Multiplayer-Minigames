using UnityEngine;

public class WoodSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float minXSpawnPos;
    [SerializeField] float maxXSpawnPos;
    [SerializeField] float ySpawnPos;
    [SerializeField] float cameraSpeed;

    [Header("Reference")]
    [SerializeField] Camera cam;
    [SerializeField] GameManager gameManager;
    [SerializeField] WoodObjectPool woodObjectPool;
    [HideInInspector] public GameObject currentWood;

    Vector3 cameraStartPos;
    float cameraStartSize;

    float spawnStart;
    float woodCount;

    void Start()
    {
        Physics2D.gravity = new Vector3(0, -9.81f, 0); // Fixes no gravity bug when replaying the game
        spawnStart = ySpawnPos;
        cameraStartPos = cam.transform.position;
        cameraStartSize = cam.orthographicSize;
        SpawnNewWood();
    }

    void Update()
    {
        UpdateCamera();
    }

    public void SpawnNewWood()
    {
        if (gameManager.currentGameState == GameManager.GameState.running)
        {
            CountWood();
            InstantiateWood();
        }
    }

    void InstantiateWood()
    {
        ySpawnPos = spawnStart + woodCount / 2;
        Vector2 woodSpawnPos = new Vector2(Random.Range(minXSpawnPos, maxXSpawnPos), ySpawnPos);

        currentWood = woodObjectPool.GetWood();
        currentWood.transform.position = woodSpawnPos;
    }

    void CountWood()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Wood");
        woodCount = gameObjects.Length;
    }

    void UpdateCamera()
    {
        Vector3 newCamPos = cameraStartPos + new Vector3(0, woodCount / 4f, 0);
        cam.transform.position = Vector3.Lerp(cam.transform.position, newCamPos, Time.deltaTime * cameraSpeed);

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, cameraStartSize + woodCount / 4f, Time.deltaTime * cameraSpeed);
    }
}
