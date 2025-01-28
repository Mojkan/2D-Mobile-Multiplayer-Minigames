using UnityEngine;
using TMPro;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] GameObject menuUI;

    [SerializeField] GameObject JoinLobbyUI;
    [SerializeField] TMP_InputField lobbyCodeInput;

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
        GameLobbyManager.Instance.CreateLobby();
    }
}
