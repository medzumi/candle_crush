using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(CanvasHelper))]
public class CanvasHelper : MonoBehaviour
{
    [SerializeField] private CanvasScaler _canvasScaler;

    private void Reset()
    {
        _canvasScaler = GetComponent<CanvasScaler>();
    }

    private void Awake()
    {
        Reset();
        CheckScreen(Screen.width, Screen.height);   
    }
    
    #if UNITY_EDITOR
    private void Update()
    {
        CheckScreen(Screen.width, Screen.height);
    }
    #endif

    private void CheckScreen(float widht, float height)
    {
        var scale = _canvasScaler.referenceResolution.x / _canvasScaler.referenceResolution.y;
        if (widht / height > scale)
        {
            _canvasScaler.matchWidthOrHeight = 1;
        }
        else
        {
            _canvasScaler.matchWidthOrHeight = 0;
        }
    }
}
