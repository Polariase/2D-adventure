using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class FadeCanvas : MonoBehaviour
{
    public Image fadeImage;
    public FadeEventSO fadeEvent;
    private void OnFadeEvent(Color target, float duration, bool fade)
    {
        fadeImage.DOBlendableColor(target, duration);
    }
    private void OnEnable()
    {
        fadeEvent.OnEventRaised += OnFadeEvent;
    }


    private void OnDisable()
    {
        fadeEvent.OnEventRaised -= OnFadeEvent;
    }
}
