using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerStatBar statBar;
    public VoidEventSO backEvent;
    public Button setting;
    public GameObject pausePanel;
    public GameObject mobileTouch;
    public GameObject gameOverPanel;
    public VoidEventSO pauseEvent;
    public SceneLoadEventSO unloadedSceneEvent;
    public CharactorEventSO healthEvent;
    public FloatEventSO syncVolumeEvent;
    public Slider volumeSlider;
    private void Awake()
    {
#if UNITY_STANDALONE
        mobileTouch.SetActive(false);
#endif
        setting.onClick.AddListener(TogglePausePanel);
    }
    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
        unloadedSceneEvent.LoadRequestEvent += OnLoadEvent;
        backEvent.OnEventRaised += OnBackEvent;
        syncVolumeEvent.OnEventRaised += OnSyncVolumeEvent;
    }
    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
        unloadedSceneEvent.LoadRequestEvent -= OnLoadEvent;
        backEvent.OnEventRaised -= OnBackEvent;
        syncVolumeEvent.OnEventRaised -= OnSyncVolumeEvent;
    }

    private void OnSyncVolumeEvent(float arg0)
    {
        volumeSlider.value = (arg0 + 80) / 100;
    }

    private void TogglePausePanel()
    {
        if(pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(false);
            Time.timeScale= 1.0f;
        }
        else
        {
            pauseEvent.RaiseEvent();
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
    private void OnBackEvent()
    {
        gameOverPanel.SetActive(false);
    }

    private void OnLoadEvent(GameSceneSO gs, Vector3 arg1, bool arg2)
    {
        if(gs.SceneType==SceneType.Menu)
        {
            statBar.gameObject.SetActive(false);
        }
        else
        {
            statBar.gameObject.SetActive(true);
        }
    }

    private void OnHealthEvent(Character character)
    {
        float percentage=character.curHp/character.maxHp;
        statBar.OnHealthChange(percentage);
    }
}
