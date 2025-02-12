using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyUIManager : MonoBehaviour
{
    [Header("Reference UI")]
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject JoinLobbyUI;
    [SerializeField] GameObject lobbyUI;
    [SerializeField] TextMeshProUGUI lobbyCodeText;

    [Header("Reference NameUI Prefab")]
    [SerializeField] GameObject userInfoUIPrefab;
    [SerializeField] Transform userInfoUIPrefabParent;

    [Header("Reference Fade Out")]
    [SerializeField] GameObject fadeOutUI;

    void Start()
    {
        GameLobbyManager.Instance.lobbyUIManager = this;
    }

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
        foreach(Transform OldUserUIObject in userInfoUIPrefabParent)
        {
            if (OldUserUIObject.name == "PlayerNameAndScore(Clone)")
            {
                Destroy(OldUserUIObject.gameObject);
            }
        }

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

    public void DisableLobbyUI()
    {
        FirebaseManager.Instance.QuitLobby();
        menuUI.SetActive(true);
        lobbyUI.SetActive(false);
    }

    public void FadeOutUI()
    {
        fadeOutUI.SetActive(true);
    }
}
