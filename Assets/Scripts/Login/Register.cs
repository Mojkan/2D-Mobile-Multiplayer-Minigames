using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class PlayerProfile
{
    public string username;
    public string created;
}

public class Register : MonoBehaviour
{
    [SerializeField] TMP_InputField emailInput;
    [SerializeField] TMP_InputField passwordInput;
    [SerializeField] TMP_InputField userNameInput;
    [SerializeField] GameObject errorPrefab;

    public void RegisterUser()
    {
        PlayerProfile newProfile = new PlayerProfile();
        newProfile.username = userNameInput.text;
        newProfile.created = System.DateTime.UtcNow.ToString("o");

        string newProfileJson = JsonUtility.ToJson(newProfile);

        FirebaseManager.Instance.RegisterNewUser(emailInput.text, passwordInput.text, newProfileJson, OnRegisterSuccess, OnRegisterFailure);
    }

    private void OnRegisterSuccess()
    {
        Debug.Log("Registered new user! Loading next scene...");
        SceneManager.LoadScene("WoodStacking");
    }

    private void OnRegisterFailure(string error)
    {
        GameObject newErrorPrefab = Instantiate(errorPrefab);
        newErrorPrefab.GetComponent<ErrorText>().DisplayErrorText("Register failed!", 3, 970);
    }
}
