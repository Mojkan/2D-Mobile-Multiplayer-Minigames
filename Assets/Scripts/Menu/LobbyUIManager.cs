using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject JoinLobbyUI;
    [SerializeField] GameObject lobbyUI;
    [SerializeField] TextMeshProUGUI lobbyCodeText;

    public void EnableLobbyUI()
    {
        menuUI.SetActive(false);
        JoinLobbyUI.SetActive(false);
        lobbyUI.SetActive(true);

        lobbyCodeText.text = "LOBBY CODE: " + FirebaseManager.Instance.savedLobbyCode;
    }
}
