using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour,IInteractable
{
    public Vector3 transportPos;
    public  SceneLoadEventSO loadEventSO;
    public GameSceneSO sceneToGo;
    public void TrigerAction()
    {
        loadEventSO.RaiseLoadRequestEvent(sceneToGo, transportPos, true);
    }
}
