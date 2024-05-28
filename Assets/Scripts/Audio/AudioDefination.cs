using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDefination : MonoBehaviour
{
    public PlayAudioEventSO playAudioEvent;
    public AudioClip clip;
    public bool playOnEnable;
    public void OnEnable()
    {
        if(playOnEnable)
        {
            PlayAudioClip();
        }
    }
    public void PlayAudioClip()
    {
        playAudioEvent.OnEventRaised(clip);
    }
}
