using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QM.Gaming
{
    public class CardIntroBehaviour : CardBehaviour
    {

        protected override void OnGameStateChanged(bool loggedIn)
        {
            myCard.SetState(loggedIn ? CardState.TODO : CardState.LOCKED);
        }
    }

}
