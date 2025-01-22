using UnityEngine;
using TMPro;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject JoinLobbyUI;
    [SerializeField] TMP_InputField lobbyCodeInput;
    [SerializeField] GameLobbyManager gameLobbyManager;    

    public void EnableEnterLobbyCodeUI()
    {
        menuUI.SetActive(false);
        JoinLobbyUI.SetActive(true);
    }

    public void DisableEnterLobbyCodeUI()
    {
        menuUI.SetActive(true);
        JoinLobbyUI.SetActive(false);
    }

    public void SendLobbyCode()
    {
        gameLobbyManager.JoinLobby(lobbyCodeInput.text);
    }
}
