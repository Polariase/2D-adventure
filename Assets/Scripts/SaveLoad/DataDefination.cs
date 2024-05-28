using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LookDev;

public class DataDefination : MonoBehaviour
{
    public PersistentType pt;
    public string ID;
    private void OnValidate()
    {
        if (pt == PersistentType.ReadWrite)
        {
            if (ID == string.Empty)
            {
                ID = System.Guid.NewGuid().ToString();
            }
        }
        else
        {
            ID=string.Empty;
        }
       
    }
}
