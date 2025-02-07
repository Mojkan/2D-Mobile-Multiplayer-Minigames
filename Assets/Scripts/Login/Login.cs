using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class Login : MonoBehaviour
{
    [SerializeField] TMP_InputField emailInput;
    [SerializeField] TMP_InputField passwordInput;
    [SerializeField] GameObject FadeOutUI;

    public void SignInUser()
    {
        FirebaseManager.Instance.SignIn(emailInput.text, passwordInput.text, OnSignInSuccess, OnSignInFailure);
        SoundManager.Instance.PlaySound("BUTTONCLICK", 0.5f);
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
}
