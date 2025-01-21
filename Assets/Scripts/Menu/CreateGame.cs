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
        MaxPlayers = maxPlayers;
        currentLobbyState = LobbyState.waitforplayers;

        Players = new List<Player>();
    }
}


public class CreateGame : MonoBehaviour
{
    public void CreateLobby()
    {
        // Create new player
        Player newUser = new Player(FirebaseManager.Instance.savedUsername, 0);

        // Create new lobby
        GameLobby newLobby = new GameLobby(2);
        // Add player to a list within the lobby instance
        newLobby.Players.Add(newUser);
        string newLobbyJson = JsonUtility.ToJson(newLobby);

        string lobbyCode = Random.Range(100000, 1000000).ToString();

        FirebaseManager.Instance.SaveCreatedGame(lobbyCode, newLobbyJson, OnCreatingLobbySuccess, OnCreatingLobbyFail);
    }

    void OnCreatingLobbySuccess()
    {
        Debug.Log("Created new lobby!");
    }

    void OnCreatingLobbyFail()
    {
        Debug.Log("Failed to create lobby");
    }
}
