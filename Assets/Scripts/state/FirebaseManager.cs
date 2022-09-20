using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using questmill.utilities;
using FirebaseWebGL.Examples.Utils;
using FirebaseWebGL.Scripts.FirebaseBridge;
using FirebaseWebGL.Scripts.Objects;
using TMPro;
using FirebaseSDKAuth = Firebase.Auth.FirebaseAuth;
using FirebaseSDKDatabase = Firebase.Database.FirebaseDatabase;
using FirebaseSDKUser = Firebase.Auth.FirebaseUser;


public class FirebaseManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")] public DependencyStatus dependencyStatus;
    public FirebaseSDKAuth auth;
    public FirebaseSDKUser User;
    public DatabaseReference db;

    //Login variables
    [Header("Login")] public TMP_InputField emailLoginField;

    public TMP_InputField passwordLoginField;
    // public TMP_Text warningLoginText;
    // public TMP_Text confirmLoginText;

    //Register variables
    [Header("Register")] public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;

    public TMP_InputField passwordRegisterVerifyField;
    // public TMP_Text warningRegisterText;

    void Awake()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                // If they are available Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        //Set the authentication instance object
        auth = FirebaseSDKAuth.DefaultInstance;
        db = FirebaseSDKDatabase.DefaultInstance.RootReference;
    }

    //Function for the login button
    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Application.platform == RuntimePlatform.WebGLPlayer
            ? LoginWebGL(emailLoginField.text, passwordLoginField.text)
            : LoginFbSdk(emailLoginField.text, passwordLoginField.text));
    }

    //Function for the register button
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Application.platform == RuntimePlatform.WebGLPlayer
            ? RegisterWebGL(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text)
            : RegisterFbSdk(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }


    private bool IsValidRegisterData(string email, string password, string username)
    {
        if (username == "")
        {
            //If the username field is blank show a warning
            // warningRegisterText.text = "Missing Username";
            Debug.Log("Missing Username");
            GCtrl.LoginFailed("Missing Username");
            return false;
        }

        if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            //If the password does not match show a warning
            // warningRegisterText.text = "Password Does Not Match!";
            Debug.Log("Passwords Do Not Match!");
            GCtrl.LoginFailed("Passwords Do Not Match!");
            return false;
        }

        return true;
    }
    
    #region Firebase SDK
    
    private IEnumerator LoginFbSdk(string email, string password)
    {
        //Call the Firebase auth signin function passing the email and password
        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        //Wait until the task completes
        yield return new WaitUntil(predicate: () => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {loginTask.Exception}");
            if (loginTask.Exception.GetBaseException() is FirebaseException firebaseEx)
            {
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Login Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WrongPassword:
                        message = "Wrong Password";
                        break;
                    case AuthError.InvalidEmail:
                        message = "Invalid Email";
                        break;
                    case AuthError.UserNotFound:
                        message = "Account does not exist";
                        break;
                }

                Debug.Log($"Error: {message} (err-code:{errorCode})");
                GCtrl.LoginFailed(message);
            }
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = loginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            GCtrl.LoginOk(User.DisplayName, User.Email, password);
        }
    }

    private IEnumerator RegisterFbSdk(string email, string password, string username)
    {
        if (IsValidRegisterData(email, password, username))
        {
            //Call the Firebase auth signin function passing the email and password
            Task<FirebaseSDKUser> registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => registerTask.IsCompleted);

            if (registerTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {registerTask.Exception}");
                if (registerTask.Exception.GetBaseException() is FirebaseException firebaseEx)
                {
                    AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                    string message = errorCode switch
                    {
                        AuthError.MissingEmail => "Missing Email",
                        AuthError.MissingPassword => "Missing Password",
                        AuthError.WeakPassword => "Weak Password",
                        AuthError.EmailAlreadyInUse => "Email Already In Use",
                        _ => "Register Failed!"
                    };

                    Debug.Log(message);
                    GCtrl.LoginFailed(message);
                }
            }
            else
            {
                //User has now been created
                //Now get the result
                User = registerTask.Result;

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = username };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var profileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => profileTask.IsCompleted);

                    if (profileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {profileTask.Exception}");
                        if (profileTask.Exception.GetBaseException() is FirebaseException firebaseEx)
                        {
                            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        }

                        // warningRegisterText.text = "Username Set Failed!";
                        Debug.Log("Username Set Failed!"); // TODO
                        GCtrl.LoginFailed("Username Set Failed!");
                    }
                    else
                    {
                        GCtrl.LoginOk(username, email, password);
                        GCtrl.GameTokenWithPlayer =
                            true; // first login that comes automatically together with registration
                        // gets the game token to the player.
                        string jsonValue = GCtrl.GetStateAsJSON();
                        string firebaseKey = GCtrl.Email.EmailAddress2FirebaseKey();
                        db.Child("teams").Child(firebaseKey).SetRawJsonValueAsync(jsonValue).ContinueWithOnMainThread(
                            t =>
                            {
                                if (t.IsFaulted)
                                {
                                    Debug.Log("Faulted..");
                                }

                                if (t.IsCanceled)
                                {
                                    Debug.Log("Cancelled..");
                                }

                                if (t.IsCompleted)
                                {
                                    Debug.Log("Completed.");
                                }
                            });
                        ;
                    }
                }
            }
        }
    }
    
    #endregion

    #region WebGL

    private IEnumerator LoginWebGL(string email, string password)
    {
        Debug.Log("Running on WebGL, but LoginWebGL not implemented yet.");
        // TODO
        return null;
    }

    private IEnumerator RegisterWebGL(string email, string password, string username)
    {
        Debug.Log("Running on WebGL, but RegisterWebGL not implemented yet.");
        // TODO
        return null;
    }


    public void DisplayInfoCallback(string callBackMsg)
    {
        Debug.Log(callBackMsg);
    }

    #endregion
}