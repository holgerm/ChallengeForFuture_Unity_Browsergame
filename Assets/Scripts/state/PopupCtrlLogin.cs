using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupCtrlLogin : MonoBehaviour
{
    public Button LoginButton;
    public Button RegisterButton;
    public GameObject TeamnameInputField;
    public GameObject VerifyPasswordInputField;

    public GameObject innerGameObject;
    
    public void Awake()
    {
        GCtrl.LoggedInStateChanged += OnLoginChanged;
    }

    private void OnLoginChanged(bool loggedIn)
    {
        innerGameObject?.SetActive(!loggedIn);
    }

    public void UseToRegister(bool useToRegister)
    {
        LoginButton.gameObject.SetActive(!useToRegister);
        RegisterButton.gameObject.SetActive(useToRegister);
        TeamnameInputField.SetActive(useToRegister);
        VerifyPasswordInputField.SetActive(useToRegister);
    }
}
