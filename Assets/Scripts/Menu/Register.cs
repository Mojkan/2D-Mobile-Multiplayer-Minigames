using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class Register : MonoBehaviour
{
    [SerializeField] TMP_InputField emailInput;
    [SerializeField] TMP_InputField passwordInput;

    public void RegisterUser()
    {
        FirebaseManager.Instance.RegisterNewUser(emailInput.text, passwordInput.text, OnRegisterSuccess, OnRegisterFailure);
    }

    private void OnRegisterSuccess()
    {
        Debug.Log("Registered new user! Loading next scene...");
        SceneManager.LoadScene("WoodStacking");
    }

    private void OnRegisterFailure(string error)
    {
        Debug.LogError("Sign-in failed: " + error);
    }
}
