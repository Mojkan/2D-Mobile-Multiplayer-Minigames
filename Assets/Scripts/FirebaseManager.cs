using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance { get; private set; }

    public string savedUsername;
    FirebaseAuth auth;
    FirebaseDatabase db;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        InitializeFirebase();
    }

    void InitializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogError(task.Exception);
                return;
            }

            auth = FirebaseAuth.DefaultInstance;
            db = FirebaseDatabase.DefaultInstance;
            Debug.Log("Firebase initialized successfully.");
        });
    }

    public void RegisterNewUser(string email, string password, string userProfile, System.Action onSuccess, System.Action<string> onFailure)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                FirebaseUser newUser = task.Result.User;

                db.RootReference.Child("users").Child(newUser.UserId).SetRawJsonValueAsync(userProfile);

                onSuccess?.Invoke();
            }
            else
            {
                onFailure?.Invoke(task.Exception?.Message);
            }
        });
    }

    public void SignIn(string email, string password, System.Action onSuccess, System.Action<string> onFailure)
    {
        Debug.Log("Starting Sign-In");
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                onFailure?.Invoke(task.Exception.Message);
            }
            else
            {
                FirebaseUser newUser = task.Result.User;
                onSuccess?.Invoke();
            }
        });
    }

    public void GetUsername(System.Action<string> onPlayerNameLoaded)
    {
        db.RootReference.Child("users").Child(auth.CurrentUser.UserId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                DataSnapshot snapshot = task.Result;

                string username = snapshot.Child("username").Value.ToString();
                savedUsername = username;
                onPlayerNameLoaded?.Invoke(username);
            }
            else
            {
                Debug.LogError("Failed to load username: " + task.Exception);
            }
        });
    }

    public void CreateNewLobby(string lobbyCode, string lobbyData, System.Action<string> OnSuccess, System.Action OnFailure)
    {
        db.RootReference.Child("gamelobbies").Child(lobbyCode).SetRawJsonValueAsync(lobbyData).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                OnSuccess?.Invoke(lobbyCode);
            }
            else
            {
                OnFailure?.Invoke();
            }
        });
    }

    public void JoinLobby(string lobbyCode, Player player, System.Action OnSuccess, System.Action OnFailure)
    {
        var playersRef = db.RootReference.Child("gamelobbies").Child(lobbyCode).Child("Players");

        playersRef.Push().SetValueAsync(player.ToDictionary()).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log($"Player {player} added to lobby {lobbyCode}");
                OnSuccess?.Invoke();
            }
            else
            {
                Debug.LogError("Failed to add player: " + task.Exception);
                OnFailure?.Invoke();
            }
        });
    }

    public void SignOut()
    {
        auth.SignOut();
        Debug.Log("User signed out");
    }
}
