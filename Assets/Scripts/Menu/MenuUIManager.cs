using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] GameObject menuUI;

    [SerializeField] GameObject JoinLobbyUI;
    [SerializeField] TMP_InputField lobbyCodeInput;

    [SerializeField] GameObject CreateLobbyUI;
    [SerializeField] Slider LobbyMaxPlayersInput;
    [SerializeField] TextMeshProUGUI MaxPlayerNumberUI;

    #region Join Window
    public void EnableEnterLobbyCodeWindow()
    {
        menuUI.SetActive(false);
        JoinLobbyUI.SetActive(true);
    }

    public void DisableEnterLobbyCodeWindow()
    {
        menuUI.SetActive(true);
        JoinLobbyUI.SetActive(false);
    }
    #endregion

    #region Create Lobby Window
    public void EnableCreateLobbyWindow()
    {
        menuUI.SetActive(false);
        CreateLobbyUI.SetActive(true);
    }

    public void DisableCreateLobbyWindow()
    {
        menuUI.SetActive(true);
        CreateLobbyUI.SetActive(false);
    }

    public void UpdateCreateLobbyMaxPlayersText()
    {
        MaxPlayerNumberUI.text = "Lobby Size: " + LobbyMaxPlayersInput.value;
    }

    #endregion

    public void FindNewLobby()
    {
        Debug.Log("Not implemented");
    }

    public void JoinNewLobby()
    {
        GameLobbyManager.Instance.JoinLobby(lobbyCodeInput.text);
    }

    public void CreateNewLobby()
    {
        GameLobbyManager.Instance.CreateLobby((int)LobbyMaxPlayersInput.value);
    }
}
