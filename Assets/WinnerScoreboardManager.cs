using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinnerScoreboardManager : MonoBehaviour
{
    [SerializeField] GameObject scoreboardLobbyUI;
    [SerializeField] TextMeshProUGUI winnerText;
    [SerializeField] GameObject userInfoUIPrefab;
    [SerializeField] Transform userInfoUIPrefabParentObject;

    public void InitializeScoreboardLobbyUI()
    {
        UpdateUserInfoUI();
    }

    public void UpdateUserInfoUI()
    {
        FirebaseManager.Instance.GetLobbyUserInfo(OnUpdateUserInfoUISuccess, OnUpdateUserInfoUIFailure);
    }

    void OnUpdateUserInfoUISuccess(List<(string name, int score)> players)
    {
        for (int i = 0; i < players.Count; i++)
        {
            players.Sort((player1, player2) => player2.score.CompareTo(player1.score));

            GameObject newUserInfoUI = Instantiate(userInfoUIPrefab);
            newUserInfoUI.transform.SetParent(userInfoUIPrefabParentObject);

            Transform firstChild = newUserInfoUI.transform.GetChild(0);
            Transform secondChild = newUserInfoUI.transform.GetChild(1);
            firstChild.GetComponent<TextMeshProUGUI>().text = players[i].name;
            secondChild.GetComponent<TextMeshProUGUI>().text = "Score: " + players[i].score.ToString();

            newUserInfoUI.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -630 - (i * 230), 0);
            newUserInfoUI.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }

    void OnUpdateUserInfoUIFailure()
    {
        Debug.Log("Failed updating lobby UI");
    }
}
