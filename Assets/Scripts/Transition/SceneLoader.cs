using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneLoader : MonoBehaviour,ISavable
{
    public Transform playerTrans;
    public Vector3 firstPos;
    public Vector3 menuPos;
    public VoidEventSO backEvent;
    public GameSceneSO menuScene;
    public SceneLoadEventSO unloadedSceneEvent;
    public float duration;
    public VoidEventSO newGameEvent;
    public SceneLoadEventSO loadEventSO;
    public VoidEventSO ASCLSO;
    public FadeEventSO fadeEvent;
    public GameSceneSO firstLoadScnene;
    private GameSceneSO curLoadedScene;
    private GameSceneSO sceneToGo;
    private Vector3 transitonPos;
    private bool isLoading;
    private bool fade;
    private void Start()
    {
        loadEventSO.RaiseLoadRequestEvent(menuScene, menuPos, true);
    }
    private void NewGame()
    {
        sceneToGo = firstLoadScnene;
        loadEventSO.RaiseLoadRequestEvent(sceneToGo, firstPos, true);
    }
    private void OnEnable()
    {
        loadEventSO.LoadRequestEvent += OnLoadRequreEvent;
        newGameEvent.OnEventRaised += NewGame;
        backEvent.OnEventRaised += OnBackEvent;
        ISavable savable = this;
        savable.RegisterSaveData();
    }
    private void OnDisable()
    {
        loadEventSO.LoadRequestEvent -= OnLoadRequreEvent;
        newGameEvent.OnEventRaised -= NewGame;
        backEvent.OnEventRaised -= OnBackEvent;
        ISavable savable2 = this;
        savable2.UnregisterSaveData();
    }

    private void OnBackEvent()
    {
        sceneToGo = menuScene;
        loadEventSO.RaiseLoadRequestEvent(sceneToGo, menuPos, true);
    }

    private void OnLoadRequreEvent(GameSceneSO sO, Vector3 vector, bool arg3)
    {
        if(isLoading) { return; }
        isLoading = true;
        sceneToGo = sO;
        transitonPos = vector;
        fade = arg3;
        if(curLoadedScene != null)
        {
            StartCoroutine(UnloadPreScene());
        }
        else
        {
            LoadScene();
        }
    }
    private IEnumerator UnloadPreScene()
    {
        if(fade)
        {
            fadeEvent.FadeIn(duration);
        }
        yield return new WaitForSeconds(1);
        unloadedSceneEvent.RaiseLoadRequestEvent(sceneToGo,transitonPos, true);
        yield return curLoadedScene.scenenRef.UnLoadScene();
        playerTrans.gameObject.SetActive(false);
        LoadScene();
    }
    private void LoadScene()
    {
        var loading =sceneToGo.scenenRef.LoadSceneAsync(UnityEngine.SceneManagement.LoadSceneMode.Additive, true);
        loading.Completed += OnLoadCompleted;
    }
    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> handle)
    {
        playerTrans.position = transitonPos;
        curLoadedScene = sceneToGo;
        if(fade)
        {
            fadeEvent.Fadeout(duration);
        }
        playerTrans.gameObject.SetActive(true);
        isLoading = false;
        if (curLoadedScene.SceneType != SceneType.Menu)
        {
            ASCLSO.RaiseEvent();
        }
    }

    public DataDefination GetDataID()
    {
        return GetComponent<DataDefination>();
    }

    public void GetSaveData(Data data)
    {
        data.SaveGameScene(curLoadedScene);
    }

    public void LoadData(Data data)
    {
        var playerID = playerTrans.GetComponent<DataDefination>().ID;
        if(data.characterPosDic.ContainsKey(playerID))
        {
            transitonPos= data.characterPosDic[playerID];
            sceneToGo = data.GetSavedScene();
            OnLoadRequreEvent(sceneToGo, transitonPos, true);
        }
    }
}
