using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuUIManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float buttonClickSoundVolume;

    [Header("Reference")]
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
        SoundManager.Instance.PlaySound("BUTTONCLICK");
    }

    public void DisableFindGameUI()
    {
        menuUI.SetActive(true);
        findGameUI.SetActive(false);
        SoundManager.Instance.PlaySound("BUTTONCLICK");
    }

    #region Quit Window
    public void EnableQuitWindow()
    {
        menuUI.SetActive(false);
        QuitUI.SetActive(true);
        SoundManager.Instance.PlaySound("BUTTONCLICK");
    }

    public void DisableQuitWindow()
    {
        menuUI.SetActive(true);
        QuitUI.SetActive(false);
        SoundManager.Instance.PlaySound("BUTTONCLICK");
    }

    public void QuitGame()
    {
        SoundManager.Instance.PlaySound("BUTTONCLICK");
        Application.Quit();
    }
    #endregion

    #region Join Window
    public void EnableEnterLobbyCodeWindow()
    {
        findGameUI.SetActive(false);
        JoinLobbyUI.SetActive(true);
        SoundManager.Instance.PlaySound("BUTTONCLICK");
    }

    public void DisableEnterLobbyCodeWindow()
    {
        findGameUI.SetActive(true);
        JoinLobbyUI.SetActive(false);
        SoundManager.Instance.PlaySound("BUTTONCLICK");
    }

    public void JoinNewLobby()
    {
        GameLobbyManager.Instance.JoinLobby(lobbyCodeInput.text);
        SoundManager.Instance.PlaySound("BUTTONCLICK");
    }
    #endregion

    #region Create Lobby Window
    public void EnableCreateLobbyWindow()
    {
        findGameUI.SetActive(false);
        CreateLobbyUI.SetActive(true);
        SoundManager.Instance.PlaySound("BUTTONCLICK");
    }

    public void DisableCreateLobbyWindow()
    {
        findGameUI.SetActive(true);
        CreateLobbyUI.SetActive(false);
        SoundManager.Instance.PlaySound("BUTTONCLICK");
    }

    public void UpdateCreateLobbyMaxPlayersText()
    {
        MaxPlayerNumberUI.text = "Lobby Size: " + LobbyMaxPlayersInput.value;
        SoundManager.Instance.PlaySound("SLIDER");
    }

    public void CreateNewLobby()
    {
        CreateLobbyUI.SetActive(false);
        SoundManager.Instance.PlaySound("BUTTONCLICK");
        GameLobbyManager.Instance.CreateLobby((int)LobbyMaxPlayersInput.value);
    }
    #endregion

}
