using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteShadow : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Reset()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        _spriteRenderer.shadowCastingMode = ShadowCastingMode.On;
    }
}
