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

    public void RegisterUser()
    {
        PlayerProfile newProfile = new PlayerProfile();
        newProfile.username = userNameInput.text;
        newProfile.created = System.DateTime.UtcNow.ToString("o");

        string newProfileJson = JsonUtility.ToJson(newProfile);

        FirebaseManager.Instance.RegisterNewUser(emailInput.text, passwordInput.text, newProfileJson, OnRegisterSuccess, OnRegisterFailure);
        SoundManager.Instance.PlaySound("BUTTONCLICK");
    }

    private void OnRegisterSuccess()
    {
        SceneManager.LoadScene("WoodStacking");
    }

    private void OnRegisterFailure(string error)
    {
        ErrorHandler.Instance.DisplayErrorMessage("Register failed!", 970, 3);
    }
}
