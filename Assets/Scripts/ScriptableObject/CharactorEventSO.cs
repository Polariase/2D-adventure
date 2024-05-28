using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Event/CharactorEventSO")]
public class CharactorEventSO : ScriptableObject
{
    public UnityAction<Character> OnEventRaised;
    public void RaiseEvent(Character character)
    {
        OnEventRaised?.Invoke(character);
    }
}
