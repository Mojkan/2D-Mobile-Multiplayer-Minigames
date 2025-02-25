using UnityEngine;
using TMPro;

public class GetUsername : MonoBehaviour
{
    void Start()
    {
        FirebaseManager.Instance.GetUsername(UpdateUsernameUI);
    }

    void UpdateUsernameUI(string username)
    {
        GetComponent<TextMeshProUGUI>().text = username;
    }
}
