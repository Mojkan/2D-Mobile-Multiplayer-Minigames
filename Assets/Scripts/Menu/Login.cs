using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class Login : MonoBehaviour
{
    [SerializeField] TMP_InputField emailInput;
    [SerializeField] TMP_InputField passwordInput;

    public void SignInUser()
    {
        FirebaseManager.Instance.SignIn(emailInput.text, passwordInput.text, OnSignInSuccess, OnSignInFailure);
    }

    private void OnSignInSuccess()
    {
        Debug.Log("Sign-in successful! Loading next scene...");
        SceneManager.LoadScene("WoodStacking");
    }

    private void OnSignInFailure(string error)
    {
        Debug.LogError("Sign-in failed: " + error);
    }
}
