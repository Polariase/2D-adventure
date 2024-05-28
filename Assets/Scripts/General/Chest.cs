using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    private SpriteRenderer sr;
    public Sprite openSprite;
    public Sprite closeSprite;
    public bool isDone;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        sr.sprite = isDone ? openSprite : closeSprite;
    }
    public void TrigerAction()
    {
        if(!isDone)
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        sr.sprite=openSprite;
        isDone = true;
        this.gameObject.tag = "Untagged";
    }
}
