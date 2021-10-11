using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthManager : MonoBehaviour
{
    private FirebaseAuth auth;
    private FirebaseUser user;
    public string displayName;
    public string emailAddress;
    public Uri photoUrl;
    public string email;
    public string password;

    void InitializeFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }
    void SignUp()
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                DebugLog("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                DebugLog("Signed in " + user.UserId);
                displayName = user.DisplayName ?? "";
                emailAddress = user.Email ?? "";
                photoUrl = user.PhotoUrl ?? null;
            }
        }
    }

    private void DebugLog(string str)
    {
        Debug.Log(str);
    }
}
