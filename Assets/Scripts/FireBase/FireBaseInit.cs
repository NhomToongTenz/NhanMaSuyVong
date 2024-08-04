using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    private DatabaseReference databaseReference;
    private FirebaseFirestore firestore;


    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;

                if (app.Options.DatabaseUrl == null)
                {
                    app.Options.DatabaseUrl = new System.Uri("https://nhanmasuyvong-default-rtdb.asia-southeast1.firebasedatabase.app/");
                    Debug.LogWarning("Database URL was not set in the Firebase config. Setting it manually.");
                }

                databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
                firestore = FirebaseFirestore.DefaultInstance;
                Debug.Log("Firebase is ready!");
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + task.Result);
            }
        });
    }

    public void WriteRealtimeData(string userId, int score)
    {
        databaseReference.Child("users").Child(userId).Child("score").SetValueAsync(score);
    }

    public void ReadRealtimeData(string userId)
    {
        databaseReference.Child("users").Child(userId).Child("score").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                int score = int.Parse(snapshot.Value.ToString());
                Debug.Log("User score: " + score);
            }
        });
    }

    public void WriteFirestoreData(string userId, int score)
    {
        DocumentReference docRef = firestore.Collection("users").Document(userId);
        Dictionary<string, object> user = new Dictionary<string, object>
        {
            { "score", score }
        };
        docRef.SetAsync(user).ContinueWithOnMainThread(task => {
            Debug.Log("User data written successfully");
        });
    }

    public void ReadFirestoreData(string userId)
    {
        DocumentReference docRef = firestore.Collection("users").Document(userId);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DocumentSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    Debug.Log("User score: " + snapshot.GetValue<int>("score"));
                }
                else
                {
                    Debug.Log("No such document!");
                }
            }
        });
    }
}