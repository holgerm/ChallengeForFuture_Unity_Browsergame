using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GCtrl
{
    #region GameState

    private static bool _gameTokenWithPlayer = false; 
    public static event Action<bool> GameTokenChanged;

    public static bool GameTokenWithPlayer
    {
        get => _gameTokenWithPlayer;
        set
        {
            if (value != _gameTokenWithPlayer)
            {
                _gameTokenWithPlayer = value;
                Action<bool> handler = GameTokenChanged; // thread safeness
                handler?.Invoke(value);
            }
        }
    }

    private static bool _upcyclingState = false;
    public static event Action<bool> UpcyclingStateChanged;

    public static bool UpcyclingState
    {
        get => _upcyclingState;
        set
        {
            if (value != _upcyclingState)
            {
                _upcyclingState = value;
                Action<bool> handler = UpcyclingStateChanged; // thread safeness
                handler?.Invoke(value);
            }
        }
    }

    private static bool _environmentState = false;
    public static event Action<bool> EnvironmentStateChanged;

    public static bool EnvironmentState
    {
        get => _environmentState;
        set
        {
            if (value != _environmentState)
            {
                _environmentState = value;
                Action<bool> handler = EnvironmentStateChanged; // thread safeness
                handler?.Invoke(value);
            }
        }
    }

    private static bool _healthState = false;
    public static event Action<bool> HealthStateChanged;

    public static bool HealthState
    {
        get => _healthState;
        set
        {
            if (value != _healthState)
            {
                _healthState = value;
                Action<bool> handler = HealthStateChanged; // thread safeness
                handler?.Invoke(value);
            }
        }
    }

    private static bool _wasteAvoidanceState = false;
    public static event Action<bool> WasteAvoidanceStateChanged;

    public static bool WasteAvoidanceState
    {
        get => _wasteAvoidanceState;
        set
        {
            if (value != _wasteAvoidanceState)
            {
                _wasteAvoidanceState = value;
                Action<bool> handler = WasteAvoidanceStateChanged; // thread safeness
                handler?.Invoke(value);
            }
        }
    }

    #endregion

    #region UserState

    private static bool _loggedIn = false;
    public static event Action<bool> LoggedInStateChanged;

    public static bool LoggedInState
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

    public static string TeamName { get; set; }
    public static string Email { get; set; }

    public static string Password { get; set; }

    public static void LoginFailed(string message)
    {
        TeamName = null;
        Email = null;
        Password = null;
        LoggedInState = false;
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