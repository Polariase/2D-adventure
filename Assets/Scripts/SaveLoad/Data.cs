using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public string sceneToSave;
    public Dictionary<string, Vector3> characterPosDic = new Dictionary<string, Vector3>();
    public Dictionary<string, float> floatData = new Dictionary<string, float>();
    public void SaveGameScene(GameSceneSO savedScene)
    {
        sceneToSave = JsonUtility.ToJson(savedScene);
    }
    public GameSceneSO GetSavedScene()
    {
        var newScene=ScriptableObject.CreateInstance<GameSceneSO>();
        JsonUtility.FromJsonOverwrite(sceneToSave, newScene);
        return newScene;
    }
}
