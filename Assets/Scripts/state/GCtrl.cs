using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using QM.Gaming;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Global Game Controller, keeps track about login state etc. and throws events when anything changes.
/// </summary>
public class GCtrl : MonoBehaviour
{
    private void Start()
    {
        LoggedInState = false; // TODO read from local data?
        LoggedInStateChanged?.Invoke(LoggedInState); // TODO implement server connection using Microsoft PlayFab ????
    }

    #region GameState
    
    public static event Action<bool> GameTokenChanged;

    public static bool GameTokenWithPlayer
    {
        get => state.tokenwithplayer;
        set
        {
            if (value != state.tokenwithplayer)
            {
                state.tokenwithplayer = value;
                Action<bool> handler = GameTokenChanged; // thread safeness
                handler?.Invoke(value);
            }
        }
    }
    
    public Card introCard;
    public Card upcyclingCard;
    public Card wasteAvoidanceCard;
    public Card healthCard;
    public Card environmentCard;

    private void setIntroCardState()
    {
        if (LoggedInState)
            introCard.state = CardState.TODO;
        else
            introCard.state = CardState.LOCKED;
    }

    public static event Action<bool> UpcyclingStateChanged;

    public static bool UpcyclingState
    {
        get => state.upcyclingState;
        set
        {
            if (value != state.upcyclingState)
            {
                state.upcyclingState = value;
                Action<bool> handler = UpcyclingStateChanged; // thread safeness
                handler?.Invoke(value);
            }
        }
    }

    public static event Action<bool> EnvironmentStateChanged;

    public static bool EnvironmentState
    {
        get => state.environmentState;
        set
        {
            if (value != state.environmentState)
            {
                state.environmentState = value;
                Action<bool> handler = EnvironmentStateChanged; // thread safeness
                handler?.Invoke(value);
            }
        }
    }

    public static event Action<bool> HealthStateChanged;

    public static bool HealthState
    {
        get => state.healthState;
        set
        {
            if (value != state.healthState)
            {
                state.healthState = value;
                Action<bool> handler = HealthStateChanged; // thread safeness
                handler?.Invoke(value);
            }
        }
    }

    public static event Action<bool> WasteAvoidanceStateChanged;

    public static bool WasteAvoidanceState
    {
        get => state.wasteAvoidanceState;
        set
        {
            if (value != state.wasteAvoidanceState)
            {
                state.wasteAvoidanceState = value;
                Action<bool> handler = WasteAvoidanceStateChanged; // thread safeness
                handler?.Invoke(value);
            }
        }
    }

    private static GameState state = new GameState();

    internal static string GetStateAsJSON()
    {
        return JsonUtility.ToJson(state);
    }
    
    internal static void SetStateFromJSON(string json)
    {
        state = JsonUtility.FromJson<GameState>(json);
    }
    
    public static string TeamName
    {
        get
        {
            return state.teamname;
        }
        set
        {
            state.teamname = value;
        }
    }

    public static string Email
    {
        get
        {
            return state.email;
        }
        set
        {
            state.email = value;
        }
    }

    public static string Password { get; set; }

   #endregion
    
    #region Local State
    
    public static void LoginFailed(string message)
    {
        TeamName = null;
        Email = null;
        Password = null;
        LoggedInState = false;
    }
    
    private static bool _loggedIn = false;
    public static event Action<bool> LoggedInStateChanged;

    private static bool LoggedInState
    {
        get => _loggedIn;
        set
        {
            if (value != _loggedIn)
            {
                _loggedIn = value;
                Action<bool> handler = LoggedInStateChanged; // thread safeness
                handler?.Invoke(value);
            }
        }
    }

    public static void LoginOk(string teamName, string email, string password)
    {
        TeamName = teamName;
        Email = email;
        Password = password;
        LoggedInState = true;
    }

    #endregion

    #region functions

    public static void StartScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void SendEmail(string body)
    {
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        SmtpServer.Timeout = 10000;
        SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        SmtpServer.UseDefaultCredentials = false;
        SmtpServer.Port = 587;

        mail.From = new MailAddress("holger.muegge@gmail.com");
        mail.To.Add(new MailAddress("mail@holgermuegge.de"));

        mail.Subject = "Test Email through C Sharp App - Subject";
        mail.Body = body;


        SmtpServer.Credentials =
            new System.Net.NetworkCredential("holger.muegge@gmail.com", "kvvvqxbyzcchjciq") as ICredentialsByHost;
        SmtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate,
            X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        };

        mail.DeliveryNotificationOptions =
            DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.OnSuccess;
        SmtpServer.Send(mail);
    }

    #endregion
}