using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using UnityEngine;

public class FirebaseAuthManager : MonoBehaviour
{
    public static FirebaseAuthManager Instance;

    private FirebaseAuth auth;
    private FirebaseUser user;

    private void Awake()
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

    private async void Start()
    {
        await InitializeFirebase();
    }

    private async Task InitializeFirebase()
    {
        var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
        if (dependencyStatus == DependencyStatus.Available)
        {
            auth = FirebaseAuth.DefaultInstance;
            Debug.Log("Firebase Auth initialized");
        }
        else
        {
            Debug.LogError($"Could not resolve Firebase dependencies: {dependencyStatus}");
        }
    }

    public async Task SignUp(string email, string password)
    {
        try
        {
            var result = await auth.CreateUserWithEmailAndPasswordAsync(email, password);
            user = result.User;
            Debug.Log($"Sign Up Success: {user.Email}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Sign Up Failed: {e.Message}");
        }
    }

    public async Task Login(string email, string password)
    {
        try
        {
            var result = await auth.SignInWithEmailAndPasswordAsync(email, password);
            user = result.User;
            Debug.Log($"Login Success: {user.Email}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Login Failed: {e.Message}");
        }
    }

    public void Logout()
    {
        if (auth != null)
        {
            auth.SignOut();
            user = null;
            Debug.Log("Logged out");
        }
    }

    public string GetUserId()
    {
        return user != null ? user.UserId : null;
    }

    public string GetUserEmail()
    {
        return user != null ? user.Email : null;
    }
}
