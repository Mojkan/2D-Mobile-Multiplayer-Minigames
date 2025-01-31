using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[System.Serializable]
public class Player
{
    public int Score;

    public Player()
    {
        Score = 0;
    }

    // Convert Player to a dictionary for Firebase compatibility
    public Dictionary<string, object> ToDictionary()
    {
        return new Dictionary<string, object>
        {
            { "Score", Score }
        };
    }
}

[System.Serializable]
public class GameLobby
{
    public List<Player> Players;
    public int MaxPlayers;
    
    public enum LobbyState
    {
        waitforplayers,
        gamerunning,
    }
    public LobbyState currentLobbyState;

    public GameLobby(int maxPlayers)
    {
        // Lobby settings
        MaxPlayers = maxPlayers;
        currentLobbyState = LobbyState.waitforplayers;

        // All players in lobby
        Players = new List<Player>();
    }
}

public class GameLobbyManager : MonoBehaviour
{
    [SerializeField] LobbyUIManager lobbyUIManager;
    [HideInInspector] public bool isPlayerInLobby;
    [HideInInspector] public bool isGameRunning;

    public static GameLobbyManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void CreateLobby()
    {
        GameLobby newLobby = new GameLobby(2);

        string newLobbyJson = JsonUtility.ToJson(newLobby);

        string lobbyCode = Random.Range(100000, 1000000).ToString();

        FirebaseManager.Instance.CreateNewLobby(lobbyCode, newLobbyJson, OnCreatingLobbySuccess, OnCreatingLobbyFail);
    }

    void OnCreatingLobbySuccess(string lobbyCode)
    {
        // Adds the creator of the lobby
        Player newPlayer = new Player();

        FirebaseManager.Instance.JoinLobby(lobbyCode, newPlayer, OnJoiningLobbySuccess, onJoiningLobbyFailure);
    }

    void OnCreatingLobbyFail()
    {
        Debug.Log("Failed to create lobby");
    }

    public void JoinLobby(string lobbyCode)
    {
        Player newPlayer = new Player();

        FirebaseManager.Instance.JoinLobby(lobbyCode, newPlayer, OnJoiningLobbySuccess, onJoiningLobbyFailure);
    }

    void OnJoiningLobbySuccess()
    {
        isPlayerInLobby = true;
        lobbyUIManager ??= GameObject.Find("LobbyUIManager").GetComponent<LobbyUIManager>();

        lobbyUIManager.InitializeLobbyUI();
    }

    void onJoiningLobbyFailure()
    {
        Debug.Log("Failed to join lobby");
    }

    public void UpdateLobbyPlayers()
    {
        lobbyUIManager ??= GameObject.Find("LobbyUIManager").GetComponent<LobbyUIManager>();

        lobbyUIManager.UpdateUserInfoUI();
    }

    public void StartGame()
    {
        FirebaseManager.Instance.StopListenToLobbyPlayersChanged();
        isGameRunning = true;
        Invoke(nameof(LoadNewScene), 5);
    }

    void LoadNewScene()
    {
        SceneManager.LoadScene("WoodStacking");
    }
}
