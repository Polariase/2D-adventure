using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public SceneLoadEventSO loadEvent;
    public VoidEventSO ASL;
    public Player_Control control;
    public VoidEventSO BackEvent;
    public Vector2 inputDirection;
    public float speed = 6;
    public float upSpeed = 15;
    public float tackleSpeed = 12;
    public float tackleCd = 1;
    public float tackleCur;
    public bool isHurt;
    public bool isDead;
    public bool isAttack;
    public bool isTackle;
    public float knockBcak;
    private Rigidbody2D rb;
    private PlayerAnimation pa;
    private PhysicsCheck pc;
    private void Awake()
    {
        control = new Player_Control();
        rb = GetComponent<Rigidbody2D>();
        control.Gameplay.Jump.started += Jump;
        control.Gameplay.Attack.started += PlayerAttack;
        control.Gameplay.Tackle.started += PlayerTackle;
        pc=GetComponent<PhysicsCheck>();
        pa = GetComponent<PlayerAnimation>();
    }

    

    private void OnEnable()
    {
        control.Enable();
        loadEvent.LoadRequestEvent += OnLoadEvent;
        ASL.OnEventRaised += OnASLEvent;
        BackEvent.OnEventRaised += OnBackEvent;
    }

    private void OnDisable()
    {
        control.Disable();
        loadEvent.LoadRequestEvent -= OnLoadEvent;
        ASL.OnEventRaised -= OnASLEvent;
        BackEvent.OnEventRaised -= OnBackEvent;
    }

    private void OnBackEvent()
    {
        isDead = false;
    }

    private void OnASLEvent()
    {
        control.Gameplay.Enable();
    }

    private void OnLoadEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        control.Gameplay.Disable();
    }

    private void Update()
    {
        inputDirection = control.Gameplay.Move.ReadValue<Vector2>();
        if(tackleCur > 0 )
            tackleCur-= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (!isHurt && !isTackle)
            Move();
    }

    private void Move()
    {
        rb.velocity = new Vector2(speed * inputDirection.x, rb.velocity.y);
        if (inputDirection.x != 0)
        {
            transform.localScale = new Vector3((inputDirection.x > 0 ? 1 : -1), 1, 1);
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (pc.onGround)
        {
            rb.AddForce(Vector3.up * upSpeed, ForceMode2D.Impulse);
        }
        else if (pc.jumpBonus > 0)
        {
            rb.AddForce(Vector3.up * upSpeed, ForceMode2D.Impulse);
            pc.jumpBonus--;
        }
    }
    
    private void PlayerTackle(InputAction.CallbackContext context)
    {
        if(tackleCur<=0)
        {
            isTackle = true;
            tackleCur += tackleCd;
            pa.PlayerTackle();
            rb.AddForce(new Vector2((transform.localScale.x > 0 ? 1 : -1) * tackleSpeed, 0), ForceMode2D.Impulse);
        }
    }

    private void PlayerAttack(InputAction.CallbackContext context)
    {
        if (pc.onWall && !pc.onGround)
            return;
        pa.PlayAttack();
        isAttack = true;
    }
    public void GetHurt(Transform other)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2((transform.position.x >= other.position.x ? 1 : -1) * knockBcak, 0), ForceMode2D.Impulse);
    }

    public void PlayerDied()
    {
        isDead = true;
        control.Gameplay.Disable();
    }
}
