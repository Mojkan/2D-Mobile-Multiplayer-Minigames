using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public string Name;
    public int Score;

    public Player(string name, int score)
    {
        Name = name;
        Score = score;
    }

    // Convert Player to a dictionary for Firebase compatibility
    public Dictionary<string, object> ToDictionary()
    {
        return new Dictionary<string, object>
        {
            { "Name", Name },
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
        Player newPlayer = new Player(FirebaseManager.Instance.savedUsername, 0);

        FirebaseManager.Instance.JoinLobby(lobbyCode, newPlayer, OnJoiningLobbySuccess, onJoiningLobbyFailure);
    }

    void OnCreatingLobbyFail()
    {
        Debug.Log("Failed to create lobby");
    }

    public void JoinLobby(string lobbyCode)
    {
        Player newPlayer = new Player(FirebaseManager.Instance.savedUsername, 0);

        FirebaseManager.Instance.JoinLobby(lobbyCode, newPlayer, OnJoiningLobbySuccess, onJoiningLobbyFailure);
    }

    void OnJoiningLobbySuccess()
    {

    }

    void onJoiningLobbyFailure()
    {
        Debug.Log("Failed to join lobby");
    }
}
