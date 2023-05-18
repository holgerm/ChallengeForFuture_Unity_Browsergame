using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTokenUI : MonoBehaviour
{
    public Image GameTokenImage;
    
    // Start is called before the first frame update
    void Start()
    {
        GCtrl.GameTokenChanged += ReflectGameTokenState;
        ReflectGameTokenState(GCtrl.GameTokenWithPlayer);
    }

    public void ReflectGameTokenState(bool gameTokenIsWithPlayer)
    {
        if (gameTokenIsWithPlayer)
        {
            GameTokenImage.color = Color.green;
        }
        else
        {
            GameTokenImage.color= Color.red;
        }
    }
}
