using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator amt;
    private Rigidbody2D rb;
    private PhysicsCheck pc;
    private PlayerController pco;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        amt = GetComponent<Animator>();
        pc= GetComponent<PhysicsCheck>();
        pco = GetComponent<PlayerController>();
    }

    void Update()
    {
        SetAnimation();
    }

    void SetAnimation()
    {
        amt.SetFloat("vX",Mathf.Abs(rb.velocity.x));
        amt.SetFloat("vY", rb.velocity.y);
        amt.SetBool("onGround", pc.onGround);
        amt.SetBool("isDead", pco.isDead);
        amt.SetBool("isAttack", pco.isAttack);
        amt.SetBool("isSlide", !pc.onGround && pc.onWall);
    }

    public void PlayHurt()
    {
        amt.SetTrigger("Hurt");
    }

    public void PlayAttack()
    {
        amt.SetTrigger("Attack");
    }

    public void PlayerTackle()
    {
        amt.SetTrigger("Tackle");
    }
}
