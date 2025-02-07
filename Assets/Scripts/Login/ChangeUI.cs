using UnityEngine;

public class ChangeUI : MonoBehaviour
{
    [SerializeField] GameObject LoginUI;
    [SerializeField] GameObject SignUpUI;

    public void ActivateSignUpUI()
    {
        SignUpUI.SetActive(true);
        LoginUI.SetActive(false);
        SoundManager.Instance.PlaySound("BUTTONCLICK", 0.5f);
    }

    public void ActivateLoginUI()
    {
        SignUpUI.SetActive(false);
        LoginUI.SetActive(true);
        SoundManager.Instance.PlaySound("BUTTONCLICK", 0.5f);
    }
}
