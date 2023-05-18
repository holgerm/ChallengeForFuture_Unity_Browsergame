using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used for serialization only. Access to GameState happens via Game Controller "GCtrl".
/// </summary>
[Serializable]
public class GameState
{
    public string email;
    public string teamname;
    public bool tokenwithplayer;
    
    public bool upcyclingState = false;
    public bool environmentState;
    public bool healthState;
    public bool wasteAvoidanceState;

}