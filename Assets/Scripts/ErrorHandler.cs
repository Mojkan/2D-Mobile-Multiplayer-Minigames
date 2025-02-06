using UnityEngine;

public class ErrorHandler : MonoBehaviour
{
    public static ErrorHandler Instance;

    [SerializeField] GameObject errorTextPrefab;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DisplayErrorMessage(string errorMessage, float position, float duration)
    {
        GameObject newErrorMessage = Instantiate(errorTextPrefab);
        ErrorText errortext = newErrorMessage.GetComponent<ErrorText>();

        errortext.errorMessage = errorMessage;
        errortext.yPosition = position;
        errortext.duration = duration;
    }
}
