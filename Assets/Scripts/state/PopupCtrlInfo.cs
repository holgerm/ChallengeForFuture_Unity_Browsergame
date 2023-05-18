using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupCtrlInfo : MonoBehaviour
{
    public TextMeshProUGUI titleUI;
    public TextMeshProUGUI infoUI;
    public string titleText;
    [TextArea] public string infoText;

    public void OnEnable()
    {
        titleUI.text = titleText;
        infoUI.text = infoText;
    }
}