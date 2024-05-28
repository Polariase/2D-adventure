using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int knockBack;
    public float normalSpeed;
    public float chaseSpeed;
    public float curSpeed;
    public bool isHurt;
    public bool isDead;
    public Vector3 facing;
    protected Rigidbody2D rb;
    protected Animator amt;
    protected PhysicsCheck pc;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        amt = GetComponent<Animator>();
        pc = GetComponent<PhysicsCheck>();
        curSpeed = normalSpeed;
    }

    private void Update()
    {
        facing.x = -transform.localScale.x;
    }
    private void FixedUpdate()
    {
        if (!isHurt && !isDead)
            Move();
    }

    virtual public void Move()
    {
        rb.velocity = new Vector2(curSpeed * facing.x, rb.velocity.y);

        amt.SetBool("isWalk", true);
        if(!pc.onGround||pc.onWall)
        {
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        }
    }

    virtual public void StartOTD(Transform other)
    {
        StartCoroutine(OnTakeDamage(other));
    }
    virtual public IEnumerator OnTakeDamage(Transform other)
    {
        isHurt = true;
        amt.SetTrigger("Hurt");
        rb.AddForce(new Vector2((other.position.x > transform.position.x ? -1 : 1) * knockBack, 0));
        yield return new WaitForSeconds(0.8f);
        isHurt=false;
    }

    virtual public void OnDeath()
    {
        amt.SetTrigger("Die");
        isDead = true;
    }

    virtual public void destr()
    {
        Destroy(this.gameObject);
    }
}
