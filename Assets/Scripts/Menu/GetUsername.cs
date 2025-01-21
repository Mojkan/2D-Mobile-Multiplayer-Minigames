using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
