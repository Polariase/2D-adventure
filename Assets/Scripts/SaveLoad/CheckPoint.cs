using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour,IInteractable
{
    private SpriteRenderer sr;
    public VoidEventSO saveGameEvent;
    public Sprite onSprite;
    public GameObject light2d;
    public Sprite offSprite;
    private bool isDone;

    public void TrigerAction()
    {
        if (!isDone)
        {
            isDone = true;
            sr.sprite = onSprite;
            light2d.SetActive(true);
            saveGameEvent.RaiseEvent();
            this.gameObject.tag = "Untagged";
        }
    }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        sr.sprite = isDone ? onSprite : offSprite;
        light2d.SetActive(isDone);
    }
}
