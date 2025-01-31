using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyWinnerUIManager : MonoBehaviour
{
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject lobbyWinnerUI;
    [SerializeField] TextMeshProUGUI lobbyWinnerText;

    [SerializeField] GameObject userInfoUIPrefab;
    [SerializeField] Transform userInfoUIPrefabParent;

    public void Start()
    {
        if (GameLobbyManager.Instance.isGameRunning)
        {
            menuUI.SetActive(false);
            lobbyWinnerUI.SetActive(true);
            UpdateUserData();
        }
    }

    public void UpdateUserData()
    {
        FirebaseManager.Instance.GetLobbyUserInfo(OnUpdateUserDataSuccess, OnUpdateUserDataFailure);
    }

    void OnUpdateUserDataSuccess(List<(string name, int score)> players)
    {
        for (int i = 0; i < players.Count; i++)
        {
            players.Sort((player1, player2) => player2.score.CompareTo(player1.score));

            GameObject newUserInfoUI = Instantiate(userInfoUIPrefab);
            newUserInfoUI.transform.SetParent(userInfoUIPrefabParent);

            Transform firstChild = newUserInfoUI.transform.GetChild(0);
            Transform secondChild = newUserInfoUI.transform.GetChild(1);
            firstChild.GetComponent<TextMeshProUGUI>().text = players[i].name;
            secondChild.GetComponent<TextMeshProUGUI>().text = "Score: " + players[i].score.ToString();

            newUserInfoUI.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -630 - (i * 230), 0);
            newUserInfoUI.GetComponent<RectTransform>().localScale = Vector3.one;
        }

        lobbyWinnerText.text = "Winner! " + players[0].name;
        GameLobbyManager.Instance.isGameRunning = false;
        GameLobbyManager.Instance.isPlayerInLobby = false;
    }

    void OnUpdateUserDataFailure()
    {
        Debug.Log("Failed updating lobby UI");
    }

    public void DisableLobbyWinnerUI()
    {
        menuUI.SetActive(true);
        lobbyWinnerUI.SetActive(false);
    }
}
