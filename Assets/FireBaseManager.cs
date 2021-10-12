using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBaseManager : MonoBehaviour
{
    private Firebase.FirebaseApp app;

    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
                // 정상적으로 왔다. 로직 실행하자.
                Debug.Log("파이어 베이스 사용가능함");

                // 실행안됨, 메인 쓰레드에서 넣어줘야함
                //GetComponent<AuthManager>().InitializeFirebase();
                lock (mainThreads)
                {
                    mainThreads.Add(() => GetComponent<AuthManager>().InitializeFirebase());
                }
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    List<Action> mainThreads = new List<Action>();
    void Update()
    {
        if (mainThreads.Count > 0)
        {
            lock (mainThreads)
            {
                foreach (var item in mainThreads)
                {
                    item();
                }
                mainThreads.Clear();
            }
        }
    }
}