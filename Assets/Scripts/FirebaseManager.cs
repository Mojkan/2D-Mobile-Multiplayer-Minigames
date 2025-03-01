using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic;
using System;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance { get; private set; }

    [HideInInspector] public string savedUsername;
    [HideInInspector] public string savedLobbyCode;
    [HideInInspector] public int savedMaxPlayers;

    FirebaseAuth auth;
    FirebaseDatabase db;

    #region FireBase Startup
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
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value; // FIXES TARGET FRAMERATE TO SCREENS REFRESHRATE
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
    #endregion

    #region Firebase AccountManagement
    public void RegisterNewUser(string email, string password, string userProfile, Action onSuccess, Action onFailure)
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
                onFailure?.Invoke();
            }
        });
    }

    public void SignIn(string email, string password, Action onSuccess, Action onFailure)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                onFailure?.Invoke();
            }
            else
            {
                FirebaseUser newUser = task.Result.User;
                onSuccess?.Invoke();
            }
        });
    }

    public void SignOut()
    {
        auth.SignOut();
    }
    #endregion

    #region Firebase Lobby Management
    public void GetUsername(Action<string> onPlayerNameLoaded)
    {
        db.RootReference.Child("users").Child(auth.CurrentUser.UserId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
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

    public void CreateNewLobby(string lobbyCode, string lobbyData, Action<string> OnSuccess, Action OnFailure)
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

    public void JoinLobby(string lobbyCode, Player player, Action OnSuccess, Action OnFailure)
    {
        // Check if the lobby exists
        db.RootReference.Child("gamelobbies").Child(lobbyCode).Child("MaxPlayers").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    savedMaxPlayers = int.Parse(snapshot.GetRawJsonValue());
                    savedLobbyCode = lobbyCode;

                    // Add player to lobby
                    db.RootReference.Child("gamelobbies").Child(lobbyCode).Child("Players").Child(savedUsername).SetValueAsync(player.ToDictionary()).ContinueWithOnMainThread(task =>
                    {
                        if (task.IsCompleted)
                        {
                            StartListenToLobbyPlayersChanged();
                            OnSuccess?.Invoke();
                        }
                    });
                }
                else
                {
                    OnFailure?.Invoke();
                }
            }
        });
    }

    public void QuitLobby()
    {
        db.RootReference.Child("gamelobbies").Child(savedLobbyCode).Child("Players").Child(savedUsername).RemoveValueAsync().ContinueWith(task =>{});
    }

    public void GetLobbyUserInfo(Action<List<(string Name, int Score)>> OnSuccess)
    {
        db.RootReference.Child("gamelobbies").Child(savedLobbyCode).Child("Players").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                var playerInfo = new List<(string Name, int Score)>();

                foreach (var snapshot in task.Result.Children)
                {
                    string name = snapshot.Key;
                    int score = int.Parse(snapshot.Child("Score").Value.ToString());

                    playerInfo.Add((name, score));
                }

                OnSuccess?.Invoke(playerInfo);
            }
        });
    }

    public void StartListenToLobbyPlayersChanged()
    {
        db.RootReference.Child("gamelobbies").Child(savedLobbyCode).Child("Players").ChildAdded += ListenToLobbyPlayersChanged;
    }

    public void StopListenToLobbyPlayersChanged()
    {
        db.RootReference.Child("gamelobbies").Child(savedLobbyCode).Child("Players").ChildAdded -= ListenToLobbyPlayersChanged;
    }

    void ListenToLobbyPlayersChanged(object sender, ChildChangedEventArgs args)
    {
        GameLobbyManager.Instance.UpdateLobbyPlayers();
        CheckIfGameShouldStart();
    }

    void CheckIfGameShouldStart()
    {
        db.RootReference.Child("gamelobbies").Child(savedLobbyCode).Child("Players").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                if (task.Result.ChildrenCount >= savedMaxPlayers)
                {
                    GameLobbyManager.Instance.StartGame();
                }
            }
        });
    }

    public void UpdatePlayerScore(int score)
    {
        db.RootReference.Child("gamelobbies").Child(savedLobbyCode).Child("Players").Child(savedUsername).Child("Score").SetValueAsync(score).ContinueWith( task=> { });
    }
    #endregion
}
