using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteSwitcher : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite spriteTrue;

    public Sprite spriteFalse;

    // Start is called before the first frame update
    private void Start()
    {
        Initialize();
    }

    private void OnValidate()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (!spriteRenderer)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SwitchTo(bool isTrue)
    {
        spriteRenderer.sprite = isTrue ? spriteTrue : spriteFalse;
    }
}