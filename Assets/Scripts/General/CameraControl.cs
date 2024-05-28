using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraControl : MonoBehaviour
{
    private CinemachineConfiner2D confiner;
    public CinemachineImpulseSource impulseSource;
    public VoidEventSO cameraShakeEvent;
    public VoidEventSO ASL;
    private void Awake()
    {
        confiner=GetComponent<CinemachineConfiner2D>();
    }
    private void OnEnable()
    {
        cameraShakeEvent.OnEventRaised += OnCameraShakeEvent;
        ASL.OnEventRaised += OnASL;
    }

    private void OnASL()
    {
        GetNewCameraBound();
    }

    private void OnCameraShakeEvent()
    {
        impulseSource.GenerateImpulse();
    }

    private void OnDisable()
    {
        cameraShakeEvent.OnEventRaised -= OnCameraShakeEvent;
    }
    private void GetNewCameraBound()
    {
        var obj = GameObject.FindGameObjectWithTag("Bound");
        if (obj == null) { return; }
        confiner.m_BoundingShape2D = obj.GetComponent<PolygonCollider2D>();
        confiner.InvalidateCache();
    }
}
