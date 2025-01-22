using UnityEngine;
using TMPro;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject JoinLobbyUI;
    [SerializeField] TMP_InputField lobbyCodeInput; 

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
        GameLobbyManager.Instance.JoinLobby(lobbyCodeInput.text);
    }
}
