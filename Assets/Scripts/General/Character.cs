using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;
public class Character : MonoBehaviour,ISavable
{
    public float maxHp;
    public float curHp;
    public VoidEventSO newGameEvent;
    public float invulnerableDur;
    public float invulnerableTime;
    public bool invulnerable = false;
    public UnityEvent<Character> OnHealthChange;
    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDie;
    private void OnEnable()
    {
        newGameEvent.OnEventRaised += NewGame;
        ISavable savable = this;
        savable.RegisterSaveData();
    }
    private void OnDisable()
    {
        newGameEvent.OnEventRaised -= NewGame;
        ISavable savable = this;
        savable.UnregisterSaveData();
    }
    private void NewGame()
    {
        curHp = maxHp;
    }

    private void Update()
    {
        if(invulnerable)
        {
            invulnerableTime -= Time.deltaTime;
            if(invulnerableTime <= 0 )
            {
                invulnerable = false;
            }
        }
    }

    public void TakeDamage(Attack other)
    {
        if (invulnerable) return;
        curHp -= other.damage;
        if (curHp <= 0)
        {
            curHp = 0;
            OnDie?.Invoke();
        }
        else
        {
            BecomeInvulnerable();
            OnTakeDamage?.Invoke(other.transform);
        }
        OnHealthChange?.Invoke(this);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water") && curHp > 0)
        {
            curHp = 0;
            OnHealthChange?.Invoke(this);
            OnDie.Invoke();
        }
    }
    private void BecomeInvulnerable()
    {
        invulnerable = true;
        invulnerableTime = invulnerableDur;
    }

    public DataDefination GetDataID()
    {
        return GetComponent<DataDefination>();
    }

    public void GetSaveData(Data data)
    {
        if (data.characterPosDic.ContainsKey(GetDataID().ID))
        {
            data.characterPosDic[GetDataID().ID] = transform.position;
            data.floatData[GetDataID().ID] = curHp;
        }
        else
        {
            data.characterPosDic.Add(GetDataID().ID, transform.position);
            data.floatData.Add(GetDataID().ID, curHp);
        }
    }

    public void LoadData(Data data)
    {
        if(data.characterPosDic.ContainsKey(GetDataID().ID))
        {
            transform.position = data.characterPosDic[GetDataID().ID];
            curHp = data.floatData[GetDataID().ID];
            OnHealthChange.Invoke(this);
        }
    }
}
