using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
    private Player_Control pc;
    private IInteractable it;
    private Animator anim;
    public GameObject signSprite;
    private bool canPress;
    private void Awake()
    {
        anim = signSprite.GetComponentInChildren<Animator>();
        pc=new Player_Control();
        pc.Enable();
        pc.Gameplay.Confirm.started += OnConfirm;
    }
    private void Update()
    {
        signSprite.SetActive(canPress);
    }
    private void OnConfirm(InputAction.CallbackContext context)
    {
        if (canPress)
        {
            it.TrigerAction();
        }
    }
    private void OnDisable()
    {
        canPress = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Interactable"))
        {
            canPress = true;
            it = other.GetComponent<IInteractable>();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        canPress = false;
    }
}
