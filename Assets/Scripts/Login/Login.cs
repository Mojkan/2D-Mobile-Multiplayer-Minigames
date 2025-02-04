using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class Login : MonoBehaviour
{
    [SerializeField] TMP_InputField emailInput;
    [SerializeField] TMP_InputField passwordInput;
    [SerializeField] GameObject errorPrefab;

    public void SignInUser()
    {
        FirebaseManager.Instance.SignIn(emailInput.text, passwordInput.text, OnSignInSuccess, OnSignInFailure);
    }

    private void OnSignInSuccess()
    {
        Debug.Log("Sign-in successful! Loading next scene...");
        SceneManager.LoadScene("Menu");
    }

    private void OnSignInFailure(string error)
    {
        GameObject newErrorPrefab = Instantiate(errorPrefab);
        newErrorPrefab.GetComponent<ErrorText>().DisplayErrorText("Sign in failed!", 3, 900);
    }
}
