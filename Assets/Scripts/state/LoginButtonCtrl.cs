using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginButtonCtrl : MonoBehaviour
{
    public TextMeshProUGUI tmpui;

    // Start is called before the first frame update
    void Start()
    {
        // register for changes:
        GCtrl.LoggedInStateChanged += SetLoginState;
        SetLoginState(GCtrl.LoggedInState);
    }

    public void SetLoginState(bool newLoginState)
    {
        if (newLoginState)
        {
            tmpui.text = $"Eingeloggt als Team {GCtrl.TeamName}";
            tmpui.color = Color.green;
        }
        else
        {
            tmpui.text = "Ihr seid nicht eingeloggt";
            tmpui.color = Color.red;
        }
    }

}
