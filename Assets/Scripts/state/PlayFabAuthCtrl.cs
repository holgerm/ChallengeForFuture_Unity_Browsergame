using System.Security.Cryptography;
using System.Text;
using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Internal;

public class PlayFabAuthCtrl : MonoBehaviour
{
    //Login variables
    [Header("Login")] public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    // public TMP_Text warningLoginText;
    // public TMP_Text confirmLoginText;

    //Register variables
    [Header("Register")] public TMP_InputField teamNameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;

    private string Encrypt(string orignal)
    {
        MD5CryptoServiceProvider cp = new MD5CryptoServiceProvider();
        byte[] bs = Encoding.UTF8.GetBytes(orignal);
        bs = cp.ComputeHash(bs);
        StringBuilder sb = new StringBuilder();
        foreach (byte b in bs)
        {
            sb.Append(b.ToString("x2").ToLower());
        }

        return sb.ToString();
    }

    public void SignUp()
    {
        var registerRequest = new RegisterPlayFabUserRequest
        {
            Email = emailRegisterField.text,
            Password = Encrypt(passwordRegisterField.text),
            Username = teamNameRegisterField.text,
            TitleId = "BF4AC"
        };
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, RegisterSuccess, RegisterError);
    }

    public void Login()
    {
        var loginRequest = new LoginWithEmailAddressRequest
        {
            Email = emailRegisterField.text,
            Password = Encrypt(passwordRegisterField.text),
            TitleId = "BF4AC"
        };
        PlayFabClientAPI.LoginWithEmailAddress(loginRequest, LoginSuccess, LoginError);
    }

    private void RegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log($"REGISTER SUCCESS: {result.Username}");
    }

    private void RegisterError(PlayFabError error)
    {
        Debug.Log($"REGISTER ERROR: {error.GenerateErrorReport()}");
    }

    private PlayFabAuthenticationContext authContext;

    private void LoginSuccess(LoginResult result)
    {
        authContext = result.AuthenticationContext;
        string playerId = result.PlayFabId;
        string username;
        GetAccountInfoRequest infoRequest = new GetAccountInfoRequest
        {
            AuthenticationContext = authContext,
            Email = emailRegisterField.text,
            PlayFabId = playerId
        };
        PlayFabClientAPI.GetAccountInfo(
            infoRequest,
            (GetAccountInfoResult getInfoRes) =>
            {
                Debug.Log($"USERNAME: {getInfoRes.AccountInfo.Username}");
                GCtrl.LoginOk(getInfoRes.AccountInfo.Username, emailRegisterField.text, passwordRegisterField.text);
                gameObject.SetActive(false);
            },
            LoginError);
    }
    
    private void LoginError(PlayFabError error)
    {
        GCtrl.LoginFailed(error.GenerateErrorReport());
    }
}