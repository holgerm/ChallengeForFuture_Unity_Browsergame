using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QM.Gaming
{
    [RequireComponent(typeof(Card))]
    public abstract class CardBehaviour : MonoBehaviour
    {
        private void Awake()
        {
            GCtrl.LoggedInStateChanged += OnGameStateChanged;
        }

        protected abstract void OnGameStateChanged(bool loggedIn);


        public Card myCard;

        public void OnValidate()
        {
            if (!myCard)
                myCard = gameObject.GetComponent<Card>();
        }

        private void Start()
        {
            if (!myCard)
                myCard = gameObject.GetComponent<Card>();
        }
    }
}