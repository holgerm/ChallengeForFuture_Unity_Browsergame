using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Game Controller Object for use as component of a gameobject in Unity.
///
/// Does not hold any state on its own, but serves as a view / controller for the GC static class in the background.
/// Provides methods and fields to be used in unity editor's inspector.
/// </summary>
public class GCO : MonoBehaviour
{
    [ReadOnly]
    public bool upcyclingState;

    private void SetState(bool value)
    {
        upcyclingState = value;
    }

    public void Awake()
    {
        GCtrl.UpcyclingStateChanged += SetState;
    }

    public void StartScene(string sceneName)
    {
        GCtrl.StartScene(sceneName);
    }

    public void SendEmail(string body)
    {
        GCtrl.SendEmail(body);
    }
}