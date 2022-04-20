using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelCtrlUpcycling : MonoBehaviour
{
    public bool upcyclingState = false;

    public SpriteSwitcher[] upcyclableObjects;

    private void Awake()
    {
        upcyclingState = GCtrl.UpcyclingState;
        GCtrl.UpcyclingStateChanged += SetState;
    }

    private void CheckArray()
    {
        upcyclableObjects = upcyclableObjects.Distinct().ToArray();
    }

    void Start()
    {
        SetStateOnAllSpriteSwitchers(GCtrl.UpcyclingState);
    }

    private void OnValidate()
    {
        CheckArray();
        
        GCtrl.UpcyclingState = upcyclingState;
        SetStateOnAllSpriteSwitchers(upcyclingState);
    }

    public void SetState(bool isGood)
    {
        if (isGood == GCtrl.UpcyclingState) return;

        GCtrl.UpcyclingState = upcyclingState;
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