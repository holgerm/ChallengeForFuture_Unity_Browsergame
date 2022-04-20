using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environmental : MonoBehaviour
{

    public GameObject good;
    public GameObject bad;
    
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
        if (!good)
        {
            good = transform.Find("Good")?.gameObject;
            
            if (!good)
                Debug.LogError("Environmental Object misses the GOOD part! You can assign it in the inspector or name a sub gameobject 'Good'.", this);
        }
        
        if (!bad)
        {
            bad = transform.Find("Bad")?.gameObject;
            
            if (!bad)
                Debug.LogError("Environmental Object misses the BAD part! You can assign it in the inspector or name a sub gameobject 'Bad'.", this);
        }
    }

    public void SetState(bool isGood)
    {
        good.SetActive(isGood);
        bad.SetActive(!isGood);
    }
    
}
