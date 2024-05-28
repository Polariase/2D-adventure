using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Audio;
using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public PlayAudioEventSO fxEvent;
    public PlayAudioEventSO bgmEvent;
    public FloatEventSO volumeEvent;
    public AudioSource bgmSource;
    public AudioMixer mixer;
    public VoidEventSO pauseEvent;
    public AudioSource fxSource;
    public FloatEventSO syncVolumeEvent;
    private void OnEnable()
    {
        fxEvent.OnEventRaised += OnFxEvent;
        bgmEvent.OnEventRaised += OnBgmEvent;
        volumeEvent.OnEventRaised += OnVolumeEvent;
        pauseEvent.OnEventRaised += OnPauseEvent;
    }

    private void OnBgmEvent(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    private void OnDisable()
    {
        fxEvent.OnEventRaised -= OnFxEvent;
        bgmEvent.OnEventRaised-= OnBgmEvent;
        volumeEvent.OnEventRaised -= OnVolumeEvent;
        pauseEvent.OnEventRaised -= OnPauseEvent;
    }

    private void OnPauseEvent()
    {
        float amount;
        mixer.GetFloat("MasterVolume", out amount);
        syncVolumeEvent.RaiseEvent(amount);
    }

    private void OnVolumeEvent(float amount)
    {
        mixer.SetFloat("MasterVolume", amount * 100 - 80);
    }

    private void OnFxEvent(AudioClip clip)
    {
        fxSource.clip = clip;
        fxSource.Play();
    }
}
