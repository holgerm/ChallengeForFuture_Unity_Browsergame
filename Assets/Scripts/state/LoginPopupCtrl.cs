using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPopupCtrl : MonoBehaviour
{
    public Button LoginButton;
    public Button RegisterButton;
    public GameObject TeamnameInputField;
    public GameObject VerifyPasswordInputField;
    
    public void UseToRegister(bool useToRegister)
    {
        LoginButton.gameObject.SetActive(!useToRegister);
        RegisterButton.gameObject.SetActive(useToRegister);
        TeamnameInputField.SetActive(useToRegister);
        VerifyPasswordInputField.SetActive(useToRegister);
    }
}
