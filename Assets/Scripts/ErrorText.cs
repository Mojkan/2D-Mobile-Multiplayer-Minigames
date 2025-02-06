using TMPro;
using UnityEngine;

public class ErrorText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI errorText;

    [HideInInspector] public string errorMessage;
    [HideInInspector] public float duration;
    [HideInInspector] public float yPosition;

    void Start()
    {
        RectTransform recTransform = gameObject.GetComponent<RectTransform>();
        recTransform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        recTransform.localScale = Vector3.one;
        recTransform.localPosition = new Vector3(0, yPosition, 0);

        errorText.text = "Error: " + errorMessage;
        Invoke(nameof(SelfDestruct), duration);
    }

    void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
