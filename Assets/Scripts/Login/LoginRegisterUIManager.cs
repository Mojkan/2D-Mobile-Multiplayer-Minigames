using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class PlayerProfile
{
    public string username;
    public string created;
}

public class LoginRegisterUIManager : MonoBehaviour
{
    [SerializeField] GameObject FadeOutUI;

    [Header("Change UI Reference")]
    [SerializeField] GameObject LoginUI;
    [SerializeField] GameObject SignUpUI;

    [Header("Login Reference")]
    [SerializeField] TMP_InputField loginEmailInput;
    [SerializeField] TMP_InputField loginPasswordInput;

    [Header("Register Reference")]
    [SerializeField] TMP_InputField registerEmailInput;
    [SerializeField] TMP_InputField registerPasswordInput;
    [SerializeField] TMP_InputField registerUserNameInput;

    #region Change UI
    public void ActivateSignUpUI()
    {
        SignUpUI.SetActive(true);
        LoginUI.SetActive(false);
        SoundManager.Instance.PlaySound("BUTTONCLICK");
    }

    public void ActivateLoginUI()
    {
        SignUpUI.SetActive(false);
        LoginUI.SetActive(true);
        SoundManager.Instance.PlaySound("BUTTONCLICK");
    }
    #endregion

    #region Login
    public void SignInUser()
    {
        FirebaseManager.Instance.SignIn(loginEmailInput.text, loginPasswordInput.text, OnSignInSuccess, OnSignInFailure);
        SoundManager.Instance.PlaySound("BUTTONCLICK");
    }

    private void OnSignInSuccess()
    {
        FadeOutUI.SetActive(true);
        Invoke(nameof(ChangeToMenuScene), 2);
    }

    private void OnSignInFailure(string error)
    {
        ErrorHandler.Instance.DisplayErrorMessage("Sign in failed!", 900, 3);
    }

    void ChangeToMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
    #endregion

    #region Register
    public void RegisterUser()
    {
        PlayerProfile newProfile = new PlayerProfile();
        newProfile.username = registerUserNameInput.text;
        newProfile.created = System.DateTime.UtcNow.ToString("o");

        string newProfileJson = JsonUtility.ToJson(newProfile);

        FirebaseManager.Instance.RegisterNewUser(registerEmailInput.text, registerPasswordInput.text, newProfileJson, OnRegisterSuccess, OnRegisterFailure);
        SoundManager.Instance.PlaySound("BUTTONCLICK");
    }

    private void OnRegisterSuccess()
    {
        SceneManager.LoadScene("Menu");
    }

    private void OnRegisterFailure(string error)
    {
        ErrorHandler.Instance.DisplayErrorMessage("Register failed!", 970, 3);
    }
    #endregion
}
