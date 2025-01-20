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

        Players = new List<Player>();
        currentLobbyState = LobbyState.waitforplayers;
    }
}


public class CreateGame : MonoBehaviour
{
    public void CreateLobby()
    {
        Player testuser = new Player("Test", 0);

        GameLobby newLobby = new GameLobby(2);
        newLobby.Players.Add(testuser);
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
