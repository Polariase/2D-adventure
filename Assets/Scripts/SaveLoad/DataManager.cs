using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-100)]
public class DataManager : MonoBehaviour
{
    private List<ISavable> savableList= new List<ISavable>();
    private Data saveData;
    public static DataManager instance;
    public VoidEventSO saveDataEvent;
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        saveData=new Data();
    }
    private void Update()
    {
        if(Keyboard.current.lKey.wasPressedThisFrame)
        {
            Load();
        }
    }
    private void OnEnable()
    {
        saveDataEvent.OnEventRaised += Save;
    }
    public void RegisterSaveData(ISavable savable)
    {
        if(!savableList.Contains(savable))
        {
            savableList.Add(savable);
        }
    }
    public void UnregisterSaveData(ISavable savable)
    {
        savableList.Remove(savable);
    }
    public void Save()
    {
        foreach(var savable in savableList)
        {
            savable.GetSaveData(saveData);
        }
    }
    public void Load()
    {
        foreach(var savable in savableList)
        {
            savable.LoadData(saveData);
        }
    }
}
