using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace QM.Gaming
{
    public class Card : MonoBehaviour
    {
        public CardBehaviour behaviour;

        public string title = "Untitled";
        public CardState state;

        public TextMeshProUGUI textUI;
        public GameObject labelUI;
        public TextMeshProUGUI labelTextUI;
        public Image topGlowImage;
        public Image itemImage;
        public Sprite itemSprite;
        public GameObject buttonGO;
        public Sprite startSprite;
        public Sprite viewSprite;
        public string sceneName;

        private Button button;
        private Image buttonBgImage;
        private TextMeshProUGUI buttonText;
        private Image buttonIconImage;

        // Start is called before the first frame update
        void Start()
        {
            Initialize();
        }

        private void OnValidate()
        {
            Initialize();
        }

        private void Initialize()
        {
            CheckForErrors();

            textUI.text = title;

            SetState(state);
        }

        private void CheckForErrors()
        {
            if (labelUI == null)
                Debug.LogError("Field labelUI must be set to the label GameObject!", gameObject);
            else
            {
                labelTextUI = labelUI.GetComponentInChildren<TextMeshProUGUI>();
                if (labelTextUI == null)
                    Debug.LogError("Label Text (TMP) component is missing!", labelUI);
            }

            if (textUI == null)
                Debug.LogError("Field textUI must be set to the text displaying the title of this card!", gameObject);

            if (topGlowImage == null)
                Debug.LogError("Field topGlowImage must be set!", gameObject);

            if (itemImage == null)
                Debug.LogError("Field itemImage must be set to the item's image component!", gameObject);

            if (buttonGO == null)
                Debug.LogError("Field button must be set to the button GameObject!", gameObject);
            else
            {
                button = buttonGO.GetComponent<Button>();
                if (button == null)
                    Debug.LogError("Button is missing the Button component!", buttonGO);

                buttonBgImage = buttonGO.GetComponent<Image>();
                if (buttonBgImage == null)
                    Debug.LogError("Button is missing the Image component!", buttonGO);

                buttonText = buttonGO.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText == null) Debug.LogError("Text (TMP) is missing within the Button GameObject!", buttonGO);

                buttonIconImage = buttonGO.transform.Find("Icon")?.GetComponentInChildren<Image>();
                if (buttonIconImage == null)
                    Debug.LogError("Image for Icon is missing within the Button GameObject!", buttonGO);
            }
        }


        internal void SetState(CardState newState)
        {
            // return;
            
            itemImage.sprite = itemSprite;

            Image labelImage = labelUI.GetComponent<Image>();
            if (labelImage)
            {
                switch (newState)
                {
                    case CardState.LOCKED:
                        labelImage.color = CardColors.Locked;
                        labelTextUI.text = CardLabelTexts.Locked;
                        labelTextUI.color = Color.gray;
                        topGlowImage.color = CardColors.Locked;
                        button.interactable = false;
                        buttonBgImage.color = CardColors.Locked;
                        buttonText.text = CardButtonTexts.Locked;
                        buttonText.color = Color.gray;
                        buttonIconImage.sprite = startSprite;
                        break;
                    case CardState.TODO:
                        labelImage.color = CardColors.Todo_Top;
                        labelTextUI.text = CardLabelTexts.Todo;
                        labelTextUI.color = CardColors.LightGray;
                        topGlowImage.color = CardColors.Todo_Top;
                        button.interactable = true;
                        buttonBgImage.color = CardColors.Todo;
                        buttonText.text = CardButtonTexts.Todo;
                        buttonText.color = CardColors.LightGray;
                        buttonIconImage.sprite = startSprite;
                        break;
                    case CardState.ATWORK:
                        labelImage.color = CardColors.AtWork_Top;
                        labelTextUI.text = CardLabelTexts.AtWork;
                        labelTextUI.color = CardColors.LightGray;
                        topGlowImage.color = CardColors.AtWork_Top;
                        button.interactable = true;
                        buttonBgImage.color = CardColors.AtWork;
                        buttonText.text = CardButtonTexts.AtWork;
                        buttonText.color = CardColors.LightGray;
                        buttonIconImage.sprite = startSprite;
                        break;
                    case CardState.DONE:
                        labelImage.color = CardColors.Done_Top;
                        labelTextUI.text = CardLabelTexts.Done;
                        labelTextUI.color = CardColors.DarkGray;
                        topGlowImage.color = CardColors.Done_Top;
                        button.interactable = true;
                        buttonBgImage.color = CardColors.Done;
                        buttonText.text = CardButtonTexts.Done;
                        buttonText.color = CardColors.DarkGray;
                        buttonIconImage.sprite = viewSprite;
                        break;
                }
            }
        }

        public void LoadTopicScene()
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public enum CardState
    {
        // ReSharper disable once InconsistentNaming
        LOCKED,
        TODO,
        ATWORK,
        DONE
    }

    public static class CardLabelTexts
    {
        public static string Locked = "LOCKED";
        public static string Todo = "TODO";
        public static string AtWork = "AT WORK";
        public static string Done = "DONE";
    }

    public static class CardColors
    {
        public static Color32 Locked = new Color32(51, 51, 51, 255);
        public static Color32 Todo = new Color32(186, 15, 21, 255);
        public static Color32 Todo_Top = new Color32(212, 17, 24, 255);
        public static Color32 AtWork = new Color32(35, 126, 207, 255);
        public static Color32 AtWork_Top = new Color32(38, 152, 255, 255);
        public static Color32 Done = new Color32(101, 157, 34, 255);
        public static Color32 Done_Top = new Color32(122, 188, 42, 255);

        public static Color32 LightGray = new Color32(217, 217, 217, 255);
        public static Color32 DarkGray = new Color32(39, 39, 39, 255);
    }

    public static class CardButtonTexts
    {
        public static string Locked = "Start";
        public static string Todo = "Start";
        public static string AtWork = "Proceed";
        public static string Done = "View";
    }
}