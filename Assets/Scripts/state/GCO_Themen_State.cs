using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GCO_Themen_State : MonoBehaviour
{
    public bool environmentState = false;
    public bool healthState = false;
    public bool wasteAvoidanceState = false;

    private void Awake()
    {
        environmentState = GCtrl.EnvironmentState;
        healthState = GCtrl.HealthState;
        wasteAvoidanceState = GCtrl.WasteAvoidanceState;
    }

    void OnValidate()
    {
        GCtrl.EnvironmentState = environmentState;
        GCtrl.HealthState = healthState;
        GCtrl.WasteAvoidanceState = wasteAvoidanceState;
        
        CheckArray();
    }
    
    public SpriteSwitcher[] upcyclableObjects;

    private void CheckArray()
    {
        upcyclableObjects = upcyclableObjects.Distinct().ToArray();
    }

    public void SetState(bool isGood)
    {
        SetStateOnAllSpriteSwitchers(isGood);
    }

    private void SetStateOnAllSpriteSwitchers(bool isGood)
    {
        if (null != upcyclableObjects)
            foreach (var go in upcyclableObjects)
            {
                go.SwitchTo(isGood);
            }
    }

}
