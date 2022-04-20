using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactive : MonoBehaviour
{

    public UnityEvent interaction;

    public void Interact()
    {
        interaction?.Invoke();
    }
}
