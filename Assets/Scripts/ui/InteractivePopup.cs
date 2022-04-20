using System.Collections;
using System.Collections.Generic;
using questmill.utilities;
using TMPro;
using UnityEngine;

public class InteractivePopup : MonoBehaviour
{
    public bool show;
    public GameObject popupCanvas;

    public string title;
    public TextMeshProUGUI titleTMPText;

    public int points;
    public TextMeshProUGUI pointsTMPText;


    [TextArea(5, 10)] public string text;
    public TextMeshProUGUI descriptionTMPText;


    public void SetShow(bool showStatus)
    {
        show = showStatus;
        popupCanvas?.SetActive(show);
    }

    public void OnValidate()
    {
        SetShow(show);
        if (descriptionTMPText)
            descriptionTMPText.text = text.Decode4TMP();
        if (titleTMPText)
            titleTMPText.text = title;
        if (pointsTMPText)
            pointsTMPText.text = points.ToString();
    }


    // Start is called before the first frame update
    public void OpenHelp()
    {
        Debug.Log("We will open help popup here ...");
    }
}