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

    [SerializeField] GameObject userInfoUIPrefab;
    [SerializeField] Transform userInfoUIPrefabParent;

    public void InitializeLobbyUI()
    {
        menuUI.SetActive(false);
        JoinLobbyUI.SetActive(false);
        lobbyUI.SetActive(true);

        lobbyCodeText.text = "LOBBY CODE: " + FirebaseManager.Instance.savedLobbyCode;
        UpdateUserInfoUI();
    }

    public void UpdateUserInfoUI()
    {
        FirebaseManager.Instance.GetLobbyUserInfo(OnUpdateUserDataSuccess, OnUpdateUserDataFailure);
    }

    void OnUpdateUserDataSuccess(List<(string name, int score)> players)
    {
        for (int i = 0; i < players.Count; i++)
        {
            GameObject newUserInfoUI = Instantiate(userInfoUIPrefab);
            newUserInfoUI.transform.SetParent(userInfoUIPrefabParent);

            Transform firstChild = newUserInfoUI.transform.GetChild(0);
            Transform secondChild = newUserInfoUI.transform.GetChild(1);
            firstChild.GetComponent<TextMeshProUGUI>().text = players[i].name;
            secondChild.GetComponent<TextMeshProUGUI>().text = "Score: " + players[i].score.ToString();

            newUserInfoUI.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -630 -(i * 230), 0);
            newUserInfoUI.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }

    void OnUpdateUserDataFailure()
    {
        Debug.Log("Failed updating lobby UI");
    }
}
