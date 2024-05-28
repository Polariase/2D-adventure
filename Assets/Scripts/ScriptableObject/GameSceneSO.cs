using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName ="Event/GameSceneSO")]
public class GameSceneSO : ScriptableObject
{
    public SceneType SceneType;
    public AssetReference scenenRef;
}
