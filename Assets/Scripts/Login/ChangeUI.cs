using UnityEngine;

public class ChangeUI : MonoBehaviour
{
    [SerializeField] GameObject LoginUI;
    [SerializeField] GameObject SignUpUI;

    public void ActivateSignUpUI()
    {
        SignUpUI.SetActive(true);
        LoginUI.SetActive(false);
    }

    public void ActivateLoginUI()
    {
        SignUpUI.SetActive(false);
        LoginUI.SetActive(true);
    }
}
