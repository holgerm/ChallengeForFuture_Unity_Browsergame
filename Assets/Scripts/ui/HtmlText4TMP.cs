using System;
using questmill.utilities;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HtmlText4TMP : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler 
{
    public TextMeshProUGUI textComponent;
    private bool m_isHoveringObject;
    private int m_selectedLink = -1;
    public RectTransform textPopupPrefab;
    private TextMeshProUGUI popupTextTMP;

    private void OnValidate()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    void Awake()
    {
        if (!textComponent)
        {
            textComponent = GetComponent<TextMeshProUGUI>();
        }

        if (textPopupPrefab)
        {
            textPopupPrefab.gameObject.SetActive(false);
            popupTextTMP = textPopupPrefab.GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var linkIndex =
            TMP_TextUtilities.FindIntersectingLink(textComponent, Input.mousePosition, null);

        if (linkIndex != -1)
        {
            // was a link clicked?
            var linkInfo = textComponent.textInfo.linkInfo[linkIndex];

            // open the link id as a url, which is the metadata we added in the text field
            string linkUrl = linkInfo.GetLinkID();
            Application.OpenURL(linkUrl);
        }
    }

    void LateUpdate()
    {
        m_isHoveringObject = false;

        if (TMP_TextUtilities.IsIntersectingRectTransform(textComponent.rectTransform, Input.mousePosition,
            Camera.main))
        {
            m_isHoveringObject = true;
        }

        if (m_isHoveringObject)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(textComponent, Input.mousePosition, Camera.current);

            // Clear previous link selection if one existed.
            if ((linkIndex == -1 && m_selectedLink != -1) || linkIndex != m_selectedLink)
            {
                m_selectedLink = -1;
            }

            // Handle new Link selection.
            if (linkIndex != -1 && linkIndex != m_selectedLink)
            {
                m_selectedLink = linkIndex;

                TMP_LinkInfo linkInfo = textComponent.textInfo.linkInfo[linkIndex];

                // Debug.Log("Link ID: \"" + linkInfo.GetLinkID() + "\"   Link Text: \"" + linkInfo.GetLinkText() + "\""); // Example of how to retrieve the Link ID and Link Text.

                Vector3 worldPointInRectangle;
                RectTransformUtility.ScreenPointToWorldPointInRectangle(textComponent.rectTransform,
                    Input.mousePosition, Camera.current, out worldPointInRectangle);

                textPopupPrefab.position = worldPointInRectangle;
                textPopupPrefab.gameObject.SetActive(true);
                popupTextTMP.text = linkInfo.GetLinkText();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_isHoveringObject = true;
    }
    
    void OnMouseEnter()
    {
        m_isHoveringObject = true;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        m_isHoveringObject = false;
    }
    
    void OnMouseExit()
    {
        m_isHoveringObject = false;
    }


}