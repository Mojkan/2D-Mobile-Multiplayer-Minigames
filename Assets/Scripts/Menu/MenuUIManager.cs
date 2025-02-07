using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject QuitUI;

    [SerializeField] GameObject findGameUI;

    [SerializeField] GameObject JoinLobbyUI;
    [SerializeField] TMP_InputField lobbyCodeInput;

    [SerializeField] GameObject CreateLobbyUI;
    [SerializeField] Slider LobbyMaxPlayersInput;
    [SerializeField] TextMeshProUGUI MaxPlayerNumberUI;

    public void EnableFindGameUI()
    {
        menuUI.SetActive(false);
        findGameUI.SetActive(true);
    }

    public void DisableFindGameUI()
    {
        menuUI.SetActive(true);
        findGameUI.SetActive(false);
    }

    #region Quit Window
    public void EnableQuitWindow()
    {
        menuUI.SetActive(false);
        QuitUI.SetActive(true);
    }

    public void DisableQuitWindow()
    {
        menuUI.SetActive(true);
        QuitUI.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

    #region Join Window
    public void EnableEnterLobbyCodeWindow()
    {
        findGameUI.SetActive(false);
        JoinLobbyUI.SetActive(true);
    }

    public void DisableEnterLobbyCodeWindow()
    {
        findGameUI.SetActive(true);
        JoinLobbyUI.SetActive(false);
    }

    public void JoinNewLobby()
    {
        GameLobbyManager.Instance.JoinLobby(lobbyCodeInput.text);
    }
    #endregion

    #region Create Lobby Window
    public void EnableCreateLobbyWindow()
    {
        findGameUI.SetActive(false);
        CreateLobbyUI.SetActive(true);
    }

    public void DisableCreateLobbyWindow()
    {
        findGameUI.SetActive(true);
        CreateLobbyUI.SetActive(false);
    }

    public void UpdateCreateLobbyMaxPlayersText()
    {
        MaxPlayerNumberUI.text = "Lobby Size: " + LobbyMaxPlayersInput.value;
    }

    public void CreateNewLobby()
    {
        CreateLobbyUI.SetActive(false);
        GameLobbyManager.Instance.CreateLobby((int)LobbyMaxPlayersInput.value);
    }
    #endregion

}
